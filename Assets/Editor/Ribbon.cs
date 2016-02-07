using System;
using UnityEditor;

[CustomEditor(typeof(Ribbon))]
public class RibbonEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Ribbon ribbon = (Ribbon)target;

		ribbon.color = EditorGUILayout.ColorField("Color", ribbon.color);
		ribbon.width = EditorGUILayout.IntField("Width", ribbon.width);
	}
}