using System.Collections;
using System.Xml.Serialization;
using UnityEngine;


public class Ribbon : MonoBehaviour, IXmlSerializable
{
	public Color color { get; set; }
	public int width { get; set; }
	public enum RibbonType {
		Additive, // 가산혼합
		Subtractive, // 감산혼합
        Covering
	};
	public RibbonType type { get; set; }

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
            case RibbonType.Covering:
                color.r = this.color.r;
                color.g = this.color.g;
                color.b = this.color.b;
                break;        
		}
	}

	public void Update()
	{
		GetComponent<Renderer>().material.color = color;
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
