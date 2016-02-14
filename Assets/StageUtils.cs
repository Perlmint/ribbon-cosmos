using System;
using UnityEditor;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;

#if UNITY_EDITOR
public static class StageUtils
{
	[MenuItem("Stage/Save")]
	/// <summary>
		/// Saves the stage on scene to file.
		/// </summary>
		public static void SaveStageOnScene()
	{
		Stage stage = findStageOnScene();

		if (stage != null)
		{
			var path = EditorUtility.SaveFilePanel("Save stage as xml",
				            "Assets/GameData",
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
	private static Stage findStageOnScene()
	{
		Stage stage = GameObject.FindObjectOfType<Stage>();

		return stage;
	}

	public static void SaveStage(Stage stage, string path)
	{
		XmlSerializer serializer = new XmlSerializer(typeof(Stage));

		using (var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, stage);
		}
	}
}
#endif