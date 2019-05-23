using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class XLuaConst 
{
	public enum ELuaLoadType
	{
		File,//文件加载，编辑器用
		Resources,//Resource加载
		AssetBundle,//资源包加载
	}

	public static ELuaLoadType LuaLoadType = ELuaLoadType.File;//Lua加载方式

	//lua脚本所在路径文件夹
	public static string LuaFilePath
	{
		get
		{
			return Application.dataPath + "/" + LuaDir;
		}
	}

	//xlua的resource文件夹
	public static string LuaResDir
	{
		get
		{
			return Application.dataPath + "/External/XLua/Resources/";
		}
	}

	//lua脚本存在的resource文件夹
	public static string LauResPath
	{
		get
		{
			return LuaResDir + LuaDir;
		}
	}

	//lua后缀
	public const string LuaSuffix = ".lua";
	//lua索引文件，用于resource下遍历文件使用
	public const string LuaReferenceFile = "XLuaReference";
	//lua子文件夹
	public const string LuaDir = "lua/";
}
