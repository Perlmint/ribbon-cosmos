using System;
using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;

public class Stage : MonoBehaviour, IXmlSerializable
{
	public Field field;
	public Color[] objectiveColors;
	private List<Ribbon> ribbons;
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
		return StageManager.Current.newRibbon();
	}

	#region IXmlSerializable implementation

	public System.Xml.Schema.XmlSchema GetSchema()
	{
		return null;
	}

	public void ReadXml(System.Xml.XmlReader reader)
	{
		
	}

	public void WriteXml(System.Xml.XmlWriter writer)
	{
		
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
