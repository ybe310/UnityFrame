using System;
using System.Collections;
using System.Collections.Generic;
using BoEngine.UI;
using UnityEngine;
using UnityEngine.UI;
using XLua;
using Object = UnityEngine.Object;

public class LuaBehaviour : MonoBehaviour
{
	[System.Serializable]
	public class BindItem
	{
		public string name;
		public string type;
		public bool isArray;
		public Object obj;
		public Object[] objs;
	}

	public string luaFile;
	public BindItem[] items;
	
	private LuaTable uiView;
	private LuaTable uiCtrl;
	private string luaText;
	private string luaClass;
	private LuaScriptManager luaMgr = LuaScriptManager.Instance;

	private Dictionary<string,LuaFunction> ctrlFuncDic = new Dictionary<string, LuaFunction>(); //缓存用到的方法，防止反复取用


	void Awake()
	{
		luaMgr.Init();
		luaMgr.Require(luaFile);

		luaClass = luaFile;
		if (luaClass.IndexOf("/") > 0)
			luaClass = luaClass.Substring(luaClass.LastIndexOf("/") + 1);

		object[] ret = luaMgr.CallFunction(luaClass + ".New");
		if (ret == null || ret.Length == 0)
		{
			return;
		}
		else
		{
			uiView = ret[0] as LuaTable;
		}

		if (uiView == null)
		{
			return;
		}

		var func = uiView.Get<LuaFunction>("GetCtrl");
		if (func!=null)
		{
			ret = func.Call(uiView);
			if (ret != null && ret.Length > 0)
			{
				uiCtrl = ret[0] as LuaTable;
			}
		}


		uiView.Set("lb", this);
		foreach (var item in items)
		{
			Type t = GetUIType(item.type);
			if (t == null)
			{
				continue;
			}

			if (t == typeof(Image))
			{
				uiView.Set(item.name, UITool.CreateUIImage(item.obj));
			}
			else if (t == typeof (Text))
			{
				uiView.Set(item.name, UITool.CreateUIText(item.obj));
			}
			else if (t == typeof(RawImage))
			{
				uiView.Set(item.name, UITool.CreateUITexture(item.obj));
			}
			else if (t == typeof(Button))
			{
				uiView.Set(item.name, UITool.CreateUIButton(item.obj));
			}
			else if (t == typeof(UIEventListener))
			{
				uiView.Set(item.name, UITool.CreateUIEventListener(item.obj));
			}
			else if (t == typeof(InputField))
			{
				uiView.Set(item.name, UITool.CreateUIInput(item.obj));
			}
		}

		CtrlCallFunc("Awake", uiView);
	}

	public Type GetUIType(string _type)
	{
		if (_type.Contains("[]"))
		{
			_type = _type.Replace("[]", "");
		}
		Type t = Type.GetType(_type + ",Assembly-CSharp");
		if (t == null)
			t = Type.GetType("UnityEngine." + _type + ",UnityEngine");
		if (t == null)
			t = Type.GetType("UnityEngine.UI." + _type + ",UnityEngine.UI");


		return t;
	}

	void Start()
    {
		CtrlCallFunc("Start");
	}

	
    void Update()
    {
		CtrlCallFunc("Update");
	}

	void OnDestroy()
	{
		CtrlCallFunc("OnDestroy");
	}

	void OnEnable()
	{
		CtrlCallFunc("OnEnable");
	}

	void OnDisable()
	{
		CtrlCallFunc("OnDisable");
	}

	//=====================================测试===================================

	[CSharpCallLua]
	public delegate Vector3 Vector3Param(Vector3 p);
	[CSharpCallLua]
	public delegate void Vector3ParamNoRet(Vector3 p);

	private Vector3Param f;
	private Vector3ParamNoRet test;

	void OnGUI()
	{
		if (GUILayout.Button("Test"))
		{
			CtrlCallFunc("Test", new Vector3(2.5f,36,22));
		}
		if (GUILayout.Button("Test2"))
		{
			uiCtrl.Get("Get", out f);
			uiCtrl.Get("Test", out test);
			var v = f(new Vector3(2.5f, 36, 22));
			test(v);
		}

	}


	void PrintVec3(Vector3 _vec)
	{
		_vec.x = 2.6f;

		Debug.Log(_vec.x);
	}

	public Vector3 Vector3ParamMethod(Vector3 p)
	{
		return p;
	}

	//===============================================================================

	public void AddClick(UIButton _btn, LuaFunction _func, params object[] _params)
	{
		if (_btn == null)
		{
			return;
		}
		_btn.AddListener(delegate(object obj)
		{
			if (_func!=null)
			{
				_func.Call(_params);
			}
		});
	}



	private void CtrlCallFunc(string _funcName, params object[] _params)
	{
		if (uiCtrl == null)
		{
			return;
		}
		if (!ctrlFuncDic.ContainsKey(_funcName))
		{
			var func = uiCtrl.Get<LuaFunction>(_funcName);
			if (func != null)
			{
				ctrlFuncDic.Add(_funcName, func);

				func.Call(_params);
			}
			else
			{
				ctrlFuncDic.Add(_funcName, null);
			}
		}
		else
		{
			var func = ctrlFuncDic[_funcName];
			if (func != null)
			{
				func.Call(_params);
			}
		}
		
	}
	
}
