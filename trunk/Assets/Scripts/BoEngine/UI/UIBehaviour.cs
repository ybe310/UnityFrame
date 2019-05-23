//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using BoEngine.UI;
//using UnityEngine;
//using Object = UnityEngine.Object;

//[Serializable]
//public class UIBehaviour: MonoBehaviour
//{
//	[SerializeField]
//	public List<UIObject> mObjectList = new List<UIObject>();
//	[SerializeField]
//	public UIViewComponent uiView;
//	[SerializeField]
//	public UILoginView loginView;

//	public Action OnAwakeFunc = null;
//	public Action OnStartFunc = null;
//	public Action OnUpdateFunc = null;
//	public Action OnFixedUpdateFunc = null;
//	public Action OnDestoryFunc = null;
//	public Action OnEnableFunc = null;
//	public Action OnDisableFunc = null;


//	void Awake()
//	{
//		if (OnAwakeFunc != null)
//		{
//			OnAwakeFunc();
//		}

//		Debug.Log(mObjectList.Count);
//	}

//	void Start()
//	{
//		if (OnStartFunc != null)
//		{
//			OnStartFunc();
//		}
		
//	}

//	void Update()
//	{
//		if (OnUpdateFunc != null)
//		{
//			OnUpdateFunc();
//		}
//	}


//	public void AddObjectComponent(string _variateName, Type _type, Object _obj)
//	{
//		if (!ContainObj(_variateName))
//		{
//			mObjectList.Add(new UIObject() { variateName = _variateName, type = _type, obj = _obj });
//		}
//	}

//	public bool ContainObj(string _variateName)
//	{
//		for (int i = 0; i<mObjectList.Count; i++)
//		{
//			if (mObjectList[i].variateName == _variateName)
//			{
//				return true;
//			}
//		}

//		return false;
//	}


//	public int GetObjectCount()
//	{
//		return mObjectList.Count;
//	}

//	public List<UIObject> GetObjectList()
//	{
//		return mObjectList;
//	}

//	public UIObject GetUIObject(string _variateName)
//	{
//		for (int i = 0; i < mObjectList.Count; i++)
//		{
//			if (mObjectList[i].variateName == _variateName)
//			{
//				return mObjectList[i];
//			}
//		}

//		return null;
//	}


//	public BoUIContainer ConvertBoUI(Type _type, Object _obj)
//	{
//		if (_type == typeof (BoUIContainer))
//		{
//			return new BoUIContainer((GameObject)_obj);
//		}
//		else if(_type == typeof(BoUIButton))
//		{
//			return new BoUIButton((GameObject)_obj);
//		}
//		else if (_type == typeof(BoUIImage))
//		{
//			return new BoUIImage((GameObject)_obj);
//		}
//		else if (_type == typeof(BoUIInput))
//		{
//			return new BoUIInput((GameObject)_obj);
//		}
//		else if (_type == typeof(BoUIText))
//		{
//			return new BoUIText((GameObject)_obj);
//		}
//		else if (_type == typeof(BoUITexture))
//		{
//			return new BoUITexture((GameObject)_obj);
//		}

//		return null;
//	}


//	public void WriteData(string _viewName)
//	{
//	    Type classType = Type.GetType(_viewName);

//		uiView = Activator.CreateInstance(classType) as UIViewComponent;

//		FieldInfo[] fields = classType.GetFields();
//		for (int i = 0; i < fields.Length; i++)
//		{
//			FieldInfo field = fields[i];
//			if (fields[i].IsDefined(typeof(UIComponentAttribute), false))
//			{
//				if (ContainObj(field.Name))
//				{
//					UIObject uiObj = GetUIObject(field.Name);

//					if (field.FieldType == typeof(BoUIContainer) || field.FieldType == typeof(BoUIButton) || field.FieldType == typeof(BoUIImage) || field.FieldType == typeof(BoUIText)
//						 || field.FieldType == typeof(BoUIInput) || field.FieldType == typeof(BoUIText) || field.FieldType == typeof(BoUITexture) || field.FieldType == typeof(BoUIEventListener))
//					{
//						field.SetValue(uiView, ConvertBoUI(field.FieldType, uiObj.obj));
//					}
//					else if (field.FieldType == typeof(GameObject))
//					{
//						field.SetValue(uiView, (GameObject)uiObj.obj);
//					}
//					else if (field.FieldType == typeof(Transform))
//					{
//						field.SetValue(uiView, ((Transform)uiObj.obj).transform);
//					}
//				}

//			}
//		}
//	}


//}