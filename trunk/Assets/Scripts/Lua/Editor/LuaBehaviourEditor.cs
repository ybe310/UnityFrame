//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using UnityEngine;
//using UnityEditor;
//using BoEngine.UI;
//using NUnit.Framework.Interfaces;
//using Object = UnityEngine.Object;

//[CanEditMultipleObjects]
//[CustomEditor(typeof(LuaBehaviour))]
//public class LuaBehaviourEditor : Editor
//{
//	SerializedProperty luaName;
//	LuaBehaviour luaBehaviour;

//	string fieldType = "";
//	string fieldName = "";
//	Dictionary<string, string> fieldDic = new Dictionary<string, string>(); 
//	Dictionary<string, bool> fieldFoldOutDic = new Dictionary<string, bool>();

//	void OnEnable()
//	{
//		luaBehaviour = target as LuaBehaviour;

//		luaName = serializedObject.FindProperty("luaFile");
//	}

//	public override void OnInspectorGUI()
//	{
//		serializedObject.Update();

//		EditorGUILayout.PropertyField(luaName, new GUIContent("Lua文件名:"));

//		fieldDic = new Dictionary<string, string>();

//		string luaPath = XLuaConst.LuaFilePath + luaName.stringValue + ".lua";
//		if (File.Exists(luaPath))
//		{
//			string[] lines = File.ReadAllLines(luaPath);
//			bool started = false;
//			for (int i = 0; i < lines.Length; i++)
//			{
//				string ln = lines[i].Trim();
//				if (!started)
//				{
//					string k = ln.TrimStart('-');
//					if (ln.StartsWith("--") && ln.TrimStart('-', ' ') == "start")
//						started = true;
//				}
//				else
//				{
//					if (ln.StartsWith("--") && ln.TrimStart('-', ' ') == "end")
//						break;

//					SplitField(ln);
//				}
//			}

//			CreateFieldObjects();
//		}
//		else
//		{
//			EditorGUILayout.HelpBox("classs is not exists:" + luaPath, MessageType.Error);
//		}
		
//		serializedObject.ApplyModifiedProperties();
//	}


//	private void SplitField(string ln)
//	{
//		int pos = ln.IndexOf("--");
//		if (pos == 0)
//		{
//			fieldType = ln.Substring(2);
//			return;
//		}

//		pos = ln.IndexOf("=");
//		if (pos > 0)
//		{
			
//			fieldName = ln.Substring(0, pos);
//		}

//		fieldDic.Add(fieldName, fieldType);
//	}

//	private void CreateFieldObjects()
//	{
//		if (fieldDic.Count > 0)
//		{
//			int idx = 0;

//			if (luaBehaviour.items == null || luaBehaviour.items.Length == 0)
//			{
//				luaBehaviour.items = new LuaBehaviour.BindItem[fieldDic.Count];
//				foreach (KeyValuePair<string, string> d in fieldDic)
//				{
//					LuaBehaviour.BindItem bind = new LuaBehaviour.BindItem();
//					bind.name = d.Key;
//					bind.type = d.Value;
//					bind.isArray = bind.type.Contains("[]");

//					luaBehaviour.items[idx] = bind;

//					idx++;
//				}
//			}
//			else
//			{
//				idx = 0;

//				var its = new LuaBehaviour.BindItem[fieldDic.Count];
//				var newFieldDic = new Dictionary<string, string>();

//				foreach (KeyValuePair<string, string> d in fieldDic)
//				{
//					bool hasContain = false;
//					for (int i = 0; i < luaBehaviour.items.Length; i++)
//					{
//						var item = luaBehaviour.items[i];
//						if (d.Key == item.name)
//						{
//							var bind = new LuaBehaviour.BindItem();
//							bind.name = item.name;
//							bind.type = d.Value;
//							bind.isArray = bind.type.Contains("[]");
//							if (bind.type == item.type)
//							{
//								bind.obj = item.obj;
//								bind.objs = item.objs;
//							}

//							its[idx] = bind;

//							idx++;

//							hasContain = true;
//							break;
//						}
//					}
//					if (!hasContain)
//					{
//						newFieldDic.Add(d.Key, d.Value);
//					}
//				}

//				if (newFieldDic.Count > 0)
//				{
//					foreach (KeyValuePair<string, string> d in newFieldDic)
//					{
//						var bind = new LuaBehaviour.BindItem();
//						bind.name = d.Key;
//						bind.type = d.Value;
//						bind.isArray = bind.type.Contains("[]");

//						its[idx] = bind;

//						idx++;
//					}
//				}

//				luaBehaviour.items = its;
//			}


//			for (int i = 0; i < luaBehaviour.items.Length; i++)
//			{
//				LuaBehaviour.BindItem bind = luaBehaviour.items[i];
//				if (bind.isArray)
//				{
//					if (!fieldFoldOutDic.ContainsKey(bind.name))
//					{
//						fieldFoldOutDic.Add(bind.name, true);
//					}
//					fieldFoldOutDic[bind.name] = EditorGUILayout.Foldout(fieldFoldOutDic[bind.name], bind.name);
//					if (fieldFoldOutDic[bind.name])
//					{
//						if (bind.objs == null || bind.objs.Length == 0)
//						{
//							bind.objs = new UnityEngine.Object[5];
//						}

//						for (int j = 0; j < 5; j++)
//						{
//							bind.objs[j] = EditorGUILayout.ObjectField("  " + bind.name + j, bind.objs[j], luaBehaviour.GetUIType(bind.type), true);
//						}
//					}
//				}
//				else
//				{
//					bind.obj = EditorGUILayout.ObjectField(bind.name, bind.obj, luaBehaviour.GetUIType(bind.type), true);
//				}

//			}
//		}
//	}
	
//}
