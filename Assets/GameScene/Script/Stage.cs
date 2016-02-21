using System;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;

public class Stage : MonoBehaviour, IXmlSerializable
{
	public Field field;
	public Field objective;
	private List<Ribbon> ribbons = new List<Ribbon>();
	public int size;

	public int Size {
		get { return size; }
		set {
			size = value;
			if (field != null)
			{
				field.Size = size;
			}
			if (objective != null)
			{
				objective.Size = size;
			}
		}
	}

	public void Start()
	{
		field = StageManager.Current.Field;
		objective = StageManager.Current.Preview;
		field.Size = size;
		objective.Size = size;
	}

	public void ApplyRibbon(Field.Direction direction, int pos, Ribbon ribbon)
	{
		field.ApplyRibbon(direction, pos, ribbon);
	}

	public Ribbon AddRibbon()
	{
		var ribbon = StageManager.Current.newRibbon();
		ribbons.Add(ribbon);
		return ribbon;
	}

	#region IXmlSerializable implementation

	public System.Xml.Schema.XmlSchema GetSchema()
	{
		return null;
	}

	public void ReadXml(System.Xml.XmlReader reader)
	{
		Size = int.Parse(reader.GetAttribute("size"));
		reader.ReadToFollowing("BaseField");
		field.ReadXml(reader);
		reader.ReadToFollowing("ObjectiveField");
		objective.ReadXml(reader);
		reader.ReadToFollowing("Ribbons");
		int ribbonCount = int.Parse(reader.GetAttribute("count"));
		for (int i = 0; i < ribbonCount; i++)
		{
			reader.ReadToFollowing("Ribbon");
			var ribbon = AddRibbon();
			ribbon.ReadXml(reader);
		}
	}

	public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString("size", size.ToString());
		writer.WriteStartElement("BaseField");
		field.WriteXml(writer);
		writer.WriteEndElement();
		writer.WriteStartElement("ObjectiveField");
		objective.WriteXml(writer);
		writer.WriteEndElement();
		writer.WriteStartElement("Ribbons");
		writer.WriteAttributeString("count", ribbons.Count.ToString());
		foreach (Ribbon ribbon in ribbons)
		{
			writer.WriteStartElement("Ribbon");
			ribbon.WriteXml(writer);
			writer.WriteEndElement();
		}
		writer.WriteEndElement();
	}

	#endregion
}
