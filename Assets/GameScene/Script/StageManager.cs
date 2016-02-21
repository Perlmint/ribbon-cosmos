using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

public class StageManager : MonoBehaviour
{
	public GameObject blockProto;
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
	public Stage stage;
	public int size;
	public float padding;

	public TextAsset StageData;

	public GameObject UiTop;
	public GameObject UiBottom;

	private List<GameObject> ribbons = new List<GameObject>();
	private Ribbon selectedRibbon = null;

	public Ribbon newRibbon()
	{
		var height = UiBottom.transform.GetComponent<RectTransform>().rect.size.y;
		var width = ribbonProto.GetComponent<Renderer>().bounds.size.x;
		GameObject ribbonObject = 
			Instantiate(ribbonProto,
				new Vector3(ribbons.Count * 270 + width / 2 + 20, height / 2, 0),
				UiBottom.transform.rotation) as GameObject;

		ribbonObject.transform.parent = UiBottom.transform;
		ribbons.Add(ribbonObject);

		return ribbonObject.GetComponent<Ribbon>();
	}

	void Start () {
		stage = GameObject.FindObjectOfType<Stage>();
		UiTop = GameObject.Find("Top");
		UiBottom = GameObject.Find("Bottom");

		if (StageData == null)
		{
			stage.Size = size;
		}
		else
		{
			string data = StageData.text;
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
			{
				using (var reader = new XmlTextReader(stream))
				{
					reader.ReadToFollowing("Stage");
					Debug.Log(reader.Name);
					stage.ReadXml(reader);
				}
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
		if (Input.GetMouseButtonUp(0))
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
}
