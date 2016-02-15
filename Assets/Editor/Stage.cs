using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Stage))]
public class StageEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Stage stage = (Stage)target;

		stage.size = EditorGUILayout.IntField("Size", stage.size);
		EditorGUILayout.LabelField("Ribbons");
		if (GUILayout.Button("Add"))
		{
			stage.AddRibbon();
		}
		EditorGUILayout.BeginVertical();
		EditorGUILayout.EndVertical();
	}
}