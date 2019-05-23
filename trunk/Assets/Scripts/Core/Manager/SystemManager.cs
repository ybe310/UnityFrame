using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager
{
	private static SystemManager instance = null;
	public static SystemManager Instance
	{
		get
		{
			if (instance == null)
				return instance = new SystemManager();
			return instance;
		}
	}

	//所有先启动的系统，保证启动顺序
	private List<ISystem> mSysList = new List<ISystem>()
	{
		LuaScriptManager.Instance,
	};


	public void InitSys()
	{
		for (int i = 0; i < mSysList.Count; i++)
		{
			mSysList[i].Init();
		}
	}

	public void UpdateSys()
	{
		for (int i = 0; i < mSysList.Count; i++)
		{
			mSysList[i].Update();
		}
	}

}
