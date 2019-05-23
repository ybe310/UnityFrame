using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Root : MonoBehaviour
{
	private const string Root_Name = "Root";

	private static Root instance;

	public static Root Instance
	{
		get
		{
			if (instance == null)
			{
				GameObject obj = GameObject.Find(Root_Name);
				if (obj != null)
				{
					instance = obj.GetComponent<Root>();
				}
			}
			return instance;
		}
	}

	public SystemManager mSysManager = SystemManager.Instance;



	void Awake()
	{
		this.gameObject.name = Root_Name;
	}

	void Start()
	{
		mSysManager.InitSys();
	}

	void Update()
	{
		mSysManager.UpdateSys();
	}
}
