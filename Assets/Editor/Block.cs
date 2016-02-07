using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Block block = (Block)target;

		block.color = EditorGUILayout.ColorField("Color", block.color);
	}
}