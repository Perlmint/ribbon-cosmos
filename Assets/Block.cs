using UnityEngine;
using System.Collections;
using UnityEditor;

/// <summary>
/// Block on stage. default color is black.
/// </summary>
public class Block : MonoBehaviour
{
	protected Color _color;

	public Color color {
		get { return _color; }
		set { _color = value; }
	}

	public Block()
	{
		color = Color.black;
	}

	public void ApplyRibbon(Ribbon ribbon)
	{
		ribbon.ApplyColor(ref this._color);
	}

	public void Update()
	{
		GetComponent<Renderer>().material.color = _color;
	}
}

[CustomEditor(typeof(Block))]
public class BlockEditor : Editor
{
	public override void OnInspectorGUI()
	{
		Block block = (Block)target;

		block.color = EditorGUILayout.ColorField("Color", block.color);
	}
}
