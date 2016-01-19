using System;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;

public class Stage : MonoBehaviour, IXmlSerializable
{
	public Field field;
	public Color[] objectiveColors;
	private List<Ribbon> ribbons = new List<Ribbon>();
	public int size;

	public int Size {
		get { return size; }
		set {
			size = value;
			field.Size = size;
			objectiveColors = new Color[size * size];
			for (int i = size * size; i > 0; i--)
			{
				objectiveColors[i - 1] = new Color();
			}
		}
	}

	public void Start()
	{
		field = StageManager.Current.Field;
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
		for (int x = 0; x < size; x++)
		{
			reader.ReadToFollowing("Row");
			for (int y = 0; y < size; y++)
			{
				reader.ReadToFollowing("Block");
				objectiveColors[y * size + x] = new Color(
					float.Parse(reader.GetAttribute("r")),
					float.Parse(reader.GetAttribute("g")),
					float.Parse(reader.GetAttribute("b")));
			}
		}
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
		for (int x = 0; x < size; x++)
		{
			writer.WriteStartElement("Row");
			for (int y = 0; y < size; y++)
			{
				writer.WriteStartElement("Block");
				var color = objectiveColors[y * size + x];
				writer.WriteAttributeString("r", color.r.ToString());
				writer.WriteAttributeString("g", color.g.ToString());
				writer.WriteAttributeString("b", color.b.ToString());
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
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
}
