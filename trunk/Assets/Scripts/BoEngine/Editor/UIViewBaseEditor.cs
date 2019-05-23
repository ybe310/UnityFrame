//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using UnityEngine.UI;
//using BoEngine.UI;
//using UnityEngine;
//using UnityEditor;

//[CanEditMultipleObjects]
//[CustomEditor(typeof(UIViewComponent), true)]
//public class UIViewBaseEditor : Editor
//{
//	SerializedProperty mViewName;


//	protected void OnEnable()
//	{
//		mViewName = serializedObject.FindProperty("ViewName");
//	}


//	public override void OnInspectorGUI()
//	{
//		serializedObject.Update();

//		UIViewComponent uiView = target as UIViewComponent;

//		EditorGUILayout.LabelField("Component");
//		EditorGUILayout.PropertyField(mViewName);

//		EditorGUILayout.BeginVertical();

//		if (!string.IsNullOrEmpty(uiView.ViewName))
//		{
//			Type classType = Assembly.GetAssembly(typeof(UIViewBase)).GetType(uiView.ViewName, true);
//			if (classType != null)
//			{
//				FieldInfo[] fields = classType.GetFields();
//				for (int i = 0; i < fields.Length; i++)
//				{
//					FieldInfo field = fields[i];
//					if (fields[i].IsDefined(typeof(UIComponentAttribute), false))
//					{
//						var obj = field.GetValue(target);

//						if (field.FieldType == typeof(BoUIContainer))
//						{
//							EditorGUILayout.ObjectField(field.Name, ((BoUIContainer)obj).gameObject, typeof(GameObject), false);
//						}
//						else if ( field.FieldType == typeof(BoUIButton) )
//						{
//							EditorGUILayout.ObjectField(field.Name, ((BoUIButton)obj).Button, typeof(Button), false);
//						}
//						else if (field.FieldType == typeof(BoUIImage) )
//						{
//							EditorGUILayout.ObjectField(field.Name, ((BoUIImage)obj).Image, typeof(BoUIImage), false);
//						}
//						else if (field.FieldType == typeof(BoUIText))
//						{
//							EditorGUILayout.ObjectField(field.Name, ((BoUIText)obj).Text, typeof(BoUIText), false);
//						}
//						else if (field.FieldType == typeof(BoUIInput))
//						{
//							EditorGUILayout.ObjectField(field.Name, ((BoUIInput)obj).InputField, typeof(BoUIInput), false);
//						}
//						else if (field.FieldType == typeof(BoUITexture))
//						{
//							EditorGUILayout.ObjectField(field.Name, ((BoUITexture)obj).RawImage, typeof(BoUITexture), false);
//						}
//						else if ( field.FieldType == typeof(BoUIEventListener))
//						{
//							EditorGUILayout.ObjectField(field.Name, ((BoUIEventListener)obj).EventListener, typeof(BoUIEventListener), false);
//						}
//						else if (field.FieldType == typeof(GameObject))
//						{
//							EditorGUILayout.ObjectField(field.Name, (GameObject)field.GetValue(obj), typeof(GameObject), false);
//						}
//						else if (field.FieldType == typeof(Transform))
//						{
//							EditorGUILayout.ObjectField(field.Name, (Transform)field.GetValue(obj), typeof(Transform), false);
//						}

//					}
//				}
//			}
			
//		}
		
//		EditorGUILayout.EndVertical();

//		serializedObject.ApplyModifiedProperties();
//	}

//}
