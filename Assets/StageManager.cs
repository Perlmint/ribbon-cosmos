using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	public GameObject UiTop;
	public GameObject UiBottom;

	private List<GameObject> ribbons = new List<GameObject>();

	public Ribbon newRibbon()
	{
		GameObject ribbonObject = 
			Instantiate(ribbonProto,
				new Vector3(ribbons.Count * 270, 0, 0),
				UiBottom.transform.rotation) as GameObject;

		ribbonObject.transform.parent = UiBottom.transform;
		ribbons.Add(ribbonObject);

		return ribbonObject.GetComponent<Ribbon>();
	}

	void Start () {
		stage = GameObject.FindObjectOfType<Stage>();
		UiTop = GameObject.Find("Top");
		UiBottom = GameObject.Find("Bottom");
		stage.Size = size;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.A))
			Temp ();
	}

	void Temp(){
		Ribbon temp;
		temp = new Ribbon(Color.red, 1, Ribbon.RibbonType.Additive);

		stage.ApplyRibbon(Field.Direction.Horizontal, 1, temp);
	}
}
