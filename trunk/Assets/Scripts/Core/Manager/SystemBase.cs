using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemBase : ISystem
{
	protected static T GetSystem<T>() where T : SystemBase, new()
	{
		T t = new T();

		return t;
	}


	public virtual void Init()
	{

	}

	public virtual void AddListener()
	{
		
	}

	public virtual void RemoveListener()
	{

	}

	public virtual void FixedUpdate()
	{
		
	}

	

	public virtual void LateUpdate()
	{
		
	}

	

	public virtual void Update()
	{
		
	}
}
