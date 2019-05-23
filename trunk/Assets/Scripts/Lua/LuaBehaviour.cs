﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class LuaBehaviour : MonoBehaviour
{
	[System.Serializable]
	public class BindItem
	{
		public string name;
		public Object value;
	}

	public string luaFile;
	public BindItem[] items;
	
	private LuaTable scriptEnv;
	private string luaText;
	private string luaClass;
	private LuaScriptManager luaMgr = LuaScriptManager.Instance;


	void Awake()
	{
		luaMgr.Init();
		//luaMgr.Require(luaFile);

		luaClass = luaFile;
		if (luaClass.IndexOf("/") > 0)
			luaClass = luaClass.Substring(luaClass.LastIndexOf("/") + 1);

		luaMgr.CallFunction("AAAA", 2);
		object[] ret = luaMgr.CallFunction(luaClass + ".New");
		if (ret == null || ret.Length == 0)
		{
			return;
		}
		else
		{
			scriptEnv = ret[0] as LuaTable;
		}

		scriptEnv.Set("lb", this);
		foreach (var injection in items)
		{
			scriptEnv.Set(injection.name, injection.value);
		}

		Debug.Log(scriptEnv.Get<LuaBehaviour>("lb").name);
		
	}

	void Start()
    {
        
    }

	
    void Update()
    {
        
    }
}