//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using UnityEngine.EventSystems;

//[CanEditMultipleObjects]
//[CustomEditor(typeof(UIBehaviour), true)]
//public class UIBehaviourEditor : Editor
//{
	


//	private void OnEnable()
//	{

//	}
	

//	public override void OnInspectorGUI()
//	{
//		serializedObject.Update();

//		UIBehaviour uiBehaviour = target as UIBehaviour;

//		EditorGUILayout.LabelField("Component");

//		EditorGUILayout.BeginVertical();
//		if (uiBehaviour.GetObjectCount() > 0)
//		{
//			List<UIObject> list = uiBehaviour.GetObjectList();
//			foreach (UIObject d in list)
//			{
//				EditorGUILayout.ObjectField(d.variateName, d.obj, d.type, false);
//			}
//		}
//		EditorGUILayout.EndVertical();

//		serializedObject.ApplyModifiedProperties();
//	}
//}
