using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
	public GameObject ribbonProto;
	private static StageManager _manager;
	public static StageManager Current {
		get {
			return _manager ?? (_manager = GameObject.FindObjectOfType<StageManager>());
		}
	}
	public static Stage CurrentStage {
		get {
			return CurrentStage;
		}
	}
	public Field Field;
	public Field Preview;
	public Stage stage;
	public int size;

	public TextAsset StageData;

	public GameObject UiTop;
	public GameObject UiBottom;

	private List<GameObject> ribbons = new List<GameObject>();
	private Ribbon selectedRibbon = null;

	public Ribbon newRibbon()
	{
		var viewSize = UiBottom.transform.GetComponent<RectTransform>().rect.size;
		var height = viewSize.y;
		var viewWidth = viewSize.x;
		var width = ribbonProto.GetComponent<Renderer>().bounds.size.x;
		GameObject ribbonObject = 
			Instantiate(ribbonProto,
				new Vector3(ribbons.Count * 270 + width / 2 + 20, height / 2, 0),
				UiBottom.transform.rotation) as GameObject;

		RectTransform contentTransform = (RectTransform)UiBottom.GetComponent<ScrollRect>().content;
		ribbonObject.transform.parent = contentTransform;
		ribbons.Add(ribbonObject);
		contentTransform.sizeDelta = new Vector2(Math.Max(viewWidth, ribbonObject.transform.localPosition.x + width), 0);

		return ribbonObject.GetComponent<Ribbon>();
	}

	void Start () {
		stage = GameObject.FindObjectOfType<Stage>();
		UiTop = GameObject.Find("Top");
		UiBottom = GameObject.Find("Bottom");

		if (StageData == null)
		{
			var levelManager = GameObject.FindObjectOfType<StageLevelManager>();
			if (levelManager != null)
			{
				StageData = levelManager.NextLevel;
			}
		}
		if (StageData == null)
		{
			#if UNITY_EDITOR
			stage.Size = size;
			return;
			#else
			throw new InvalidDataException("Next stage is not specified");
			#endif
		}

		string data = StageData.text;
		using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
		{
			using (var reader = new XmlTextReader(stream))
			{
				reader.ReadToFollowing("Stage");
				stage.ReadXml(reader);
			}
		}
	}

	private Vector3 beginPosition;
	private Vector3 endPosition;

	void Update () {
		bool checkInput = false;
		if (Input.GetMouseButtonDown(0))
		{
			checkInput = true;
			beginPosition = Input.mousePosition;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			checkInput = true;
			endPosition = Input.mousePosition;
		}
		else if (Input.GetMouseButton(0))
		{
		}

		if (!checkInput)
		{
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Field.GetComponent<Collider>().Raycast(ray, out hitInfo, float.PositiveInfinity))
		{
			ProcessFieldInput(ray);
		}
		else if (Input.GetMouseButtonUp(0))
		{
			if ((endPosition - beginPosition).magnitude > 50)
			{
				// ignore too much moved
				return;
			}
			foreach(var ribbon in ribbons)
			{
				if (ribbon.GetComponent<Collider>().Raycast(ray, out hitInfo, float.PositiveInfinity))
				{
					selectedRibbon = ribbon.GetComponent<Ribbon>();
					return;
				}
			}
		}
	}

	void ProcessFieldInput(Ray ray)
	{
		if (selectedRibbon == null)
		{
			return;
		}

		Vector2 touched = Field.InputTest(ray);
		if (touched.x == float.NaN)
		{
			return;
		}

		if (Input.GetMouseButtonDown(0))
		{
			beginPosition = touched;
		}
		else if (Input.GetMouseButtonUp(0))
		{
			endPosition = touched;
			int index = 0;
			Field.Direction direction = Field.Direction.Horizontal;
			if (beginPosition.y == endPosition.y)
			{
				if (Math.Abs(beginPosition.x - endPosition.x) != Field.Size - 1)
				{
					return;
				}
				direction = Field.Direction.Horizontal;
				index = (int)beginPosition.y;
			}
			else if (beginPosition.x == endPosition.x)
			{
				if (Math.Abs(beginPosition.y - endPosition.y) != Field.Size - 1)
				{
					return;
				}
				direction = Field.Direction.Vertical;
				index = (int)beginPosition.x;
			}
			else
			{
				return;
			}

			stage.ApplyRibbon(direction, index, selectedRibbon);
		}
	}
}
