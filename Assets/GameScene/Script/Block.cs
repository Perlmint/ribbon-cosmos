using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml.Serialization;

/// <summary>
/// Block on stage. default color is black.
/// </summary>
public class Block : MonoBehaviour, IXmlSerializable
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

	#region IXmlSerializable implementation

	public System.Xml.Schema.XmlSchema GetSchema()
	{
		return null;
	}

	public void ReadXml(System.Xml.XmlReader reader)
	{
		color = new Color(
			float.Parse(reader.GetAttribute("r")),
			float.Parse(reader.GetAttribute("g")),
			float.Parse(reader.GetAttribute("b")));
	}

	public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString("r", color.r.ToString());
		writer.WriteAttributeString("g", color.g.ToString());
		writer.WriteAttributeString("b", color.b.ToString());
	}

	#endregion
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
