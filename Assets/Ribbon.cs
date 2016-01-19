using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml.Serialization;


public class Ribbon : MonoBehaviour, IXmlSerializable
{
	private Color _color;
	public Color color { get { return _color; } private set { _color = value; } }
	public int width { get; private set; }
	public enum RibbonType {
		Additive, // 가산혼합
		Subtractive // 감산혼합
	};
	public RibbonType type { get; private set; }

	public Ribbon(Color color, int width, RibbonType ribbonType)
	{
		this.color = color;
		this.width = width;
		this.type = ribbonType;
	}

	public void ApplyColor(ref Color color)
	{
		switch (type) {
		case RibbonType.Additive:
			color.r += this.color.r;
			color.g += this.color.g;
			color.b += this.color.b;
			break;
		case RibbonType.Subtractive:
			break;
		}
	}

	public void Update()
	{
		GetComponent<Renderer>().material.color = color;
	}

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
			float.Parse(reader.GetAttribute("b"))
		);
		width = int.Parse(reader.GetAttribute("width"));
	}

	public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString("r", color.r.ToString());
		writer.WriteAttributeString("g", color.g.ToString());
		writer.WriteAttributeString("b", color.b.ToString());
		writer.WriteAttributeString("width", width.ToString());
	}

	#endregion
}
