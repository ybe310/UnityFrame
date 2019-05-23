using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using XLua;

public class LuaScriptManager : SystemBase
{
    
    private static LuaScriptManager instance = null;

    public static LuaScriptManager Instance
    {
        get
        {
            if(instance == null)
            {
	            instance = GetSystem<LuaScriptManager>();
            }
            return instance;
        }
    }

	private static float lastGCTime = 0;
	private const float GCInterval = 1;

	private LuaEnv luaEnv = null;

	public LuaEnv LuaEnv
	{
		get
		{
			if (luaEnv == null)
			{
				luaEnv = new LuaEnv();
				LoadXLua();
			}
			return luaEnv;
		}
	}


	public override void Init()
	{
		base.Init();

		if (luaEnv == null)
		{
			luaEnv = new LuaEnv();
			LoadXLua();
		}
	}


	public override void Update()
	{
		base.Update();

		
		if (luaEnv != null)
		{
			if (Time.time - lastGCTime > GCInterval)
			{
				luaEnv.Tick();
				lastGCTime = Time.time;
			}
		}
	}


	private void LoadXLua()
	{
		if (XLuaConst.LuaLoadType == XLuaConst.ELuaLoadType.File)
		{
#if UNITY_EDITOR
			List<FileInfo> fileList = FindDirectoryFiles(XLuaConst.LuaFilePath, XLuaConst.LuaSuffix);
			for (int i = 0; i < fileList.Count; i++)
			{
				string filePath = fileList[i].FullName;
				byte[] bytes = System.IO.File.ReadAllBytes(filePath);
				if (bytes != null && bytes.Length > 0)
				{
					filePath = filePath.Replace("\\", "/");
					filePath = filePath.Replace(XLuaConst.LuaFilePath, "");
					filePath = filePath.Replace(XLuaConst.LuaSuffix, "");
					filePath = filePath.Replace("/", ".");

					LoadLuaFile(bytes, filePath);
				}
			}
#else
                XLuaConst.LuaLoadType = XLuaConst.ELuaLoadType.Resources;
                LoadXLua();
#endif
		}
		else if (XLuaConst.LuaLoadType == XLuaConst.ELuaLoadType.Resources)
		{
			TextAsset refText = Resources.Load<TextAsset>(XLuaConst.LuaReferenceFile);

			string[] files = refText.text.Split('\n');

			TextAsset[] texts = Resources.LoadAll<TextAsset>(XLuaConst.LuaDir);
			for (int i = 0; i < texts.Length; i++)
			{
				LoadLuaFile(texts[i].bytes, files[i].Trim().Replace("/", "."));
			}
		}
		else if (XLuaConst.LuaLoadType == XLuaConst.ELuaLoadType.AssetBundle)
		{
			//$bao 后续实现AB包加载机制
		}
		
		luaEnv.DoString(@"require 'Main'");
	}
	private List<FileInfo> FindDirectoryFiles(string path, params string[] extension)
	{
		List<FileInfo> tFileList = new List<FileInfo>();
		DirectoryInfo tDir = new DirectoryInfo(path);
		if (tDir.Exists)
		{
			FileInfo[] tFiles = tDir.GetFiles("*", SearchOption.AllDirectories);
			for (int i = 0; i < tFiles.Length; i++)
			{
				FileInfo file = tFiles[i];
				for (int j = 0; j < extension.Length; j++)
				{
					if (file.Extension.Equals(extension[j]))
					{
						tFileList.Add(file);
					}
				}
			}
		}

		return tFileList;
	}
	private void LoadLuaFile(byte[] _bytes, string _fileName)
	{
		if (luaEnv == null)
			return;
		luaEnv.AddLoader((ref string filename) =>
		{
			if (filename.ToLower() == _fileName.ToLower())
			{
				return _bytes;
			}

			return null;
		});
	}


	public object[] CallFunction(string _funcName, params object[] _args)
	{
		return CallFunction(_funcName, _args, null);
	}

	public object[] CallFunction(string _funcName, object[] _args, Type[] _returnTypes)
	{
		if (luaEnv != null)
		{
			LuaFunction luaFuc = luaEnv.Global.GetInPath<LuaFunction>(_funcName);
			if (luaFuc!=null)
			{
				return luaFuc.Call(_args, _returnTypes);
			}
		}
		return null;
	}


	public LuaTable GetTable(string _tableName)
	{
		if (luaEnv != null)
		{
			return luaEnv.Global.GetInPath<LuaTable>(_tableName);
		}
		return null;
	}


	public void Require(string _fileName)
	{
		if (luaEnv != null)
		{
			_fileName = _fileName.Replace("/", ".");
			luaEnv.DoString("require '" + _fileName + "'");
		}
	}

}
