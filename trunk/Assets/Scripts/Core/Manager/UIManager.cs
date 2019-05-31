using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SystemBase
{
	private static UIManager instance = null;

	public static UIManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = GetSystem<UIManager>();
			}
			return instance;
		}
	}


	public override void Init()
	{
		base.Init();

		
	}
}
