using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Text;

public class StageManager : MonoBehaviour
{
	public GameObject blockProto;
    public GameObject blockObjective;
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

	public TextAsset StageData;

	public GameObject UiTop;
	public GameObject UiBottom;

	private List<GameObject> ribbons = new List<GameObject>();

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

    void DrawObj()
    {
        var edge = UiTop.transform.GetComponent<RectTransform>().rect.size.y;
        float delta = edge / size;

        for(int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {

                GameObject blockObj =
                    Instantiate(blockObjective, new Vector3(delta * (x + 0.5f), 1620 + delta * (0.5f + y), -1),
                    UiTop.transform.rotation) as GameObject;
                blockObj.GetComponent<Transform>().localScale = new Vector3(delta, delta, 1.0f);

                blockObj.transform.parent = UiTop.transform;
                blockObj.GetComponent<ObjBlock>().color = stage.objectiveColors[x + size * y];
            }
        }
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

        DrawObj();
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