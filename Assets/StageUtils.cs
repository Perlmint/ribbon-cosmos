using System;
using UnityEditor;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public static class StageUtils
{
	[MenuItem("Stage/Save")]
	/// <summary>
		/// Saves the stage on scene to file.
		/// </summary>
		public static void SaveStageOnScene()
	{
		Field stage = findStageOnScene();

		if (stage != null)
		{
			var path = EditorUtility.SaveFilePanel("Save stage as xml",
				            "Assets/stages",
				            "stage.xml",
				            "xml");
			if (path.Length != 0)
			{
				SaveStage(stage, path);
			}
		}
	}

	/// <summary>
	/// Finds the game stage on scene.
	/// </summary>
	/// <returns>The game stage on scene.</returns>
	private static Field findStageOnScene()
	{
		Field stage = GameObject.FindObjectOfType<Field>();

		return stage;
	}

	public static void SaveStage(Field stage, string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(Field));

		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, stage);
		}
	}
}

