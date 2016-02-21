using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Xml;

public class StageSelectManager : MonoBehaviour
{
	public class StageInfo
	{
		public string Title { get; set; }
		public string Path { get; set; }
		public TextAsset Data { get; set; }
	}
	public List<StageInfo> StageList { get; set; }
	public GameObject StageButtonProto;
	private StageLevelManager levelManager;
	public GameObject ButtonsWrap;
	public GameObject ScrollView;

	public StageSelectManager()
	{
		StageList = new List<StageInfo>();
	}

	void Start () {
		levelManager = GameObject.FindObjectOfType<StageLevelManager>();
		TextAsset StageListData = Resources.Load<TextAsset>(Constants.StageListPath);

		// When list file is exists... load stage list from it.
		if (StageListData != null)
		{
			
			using (var reader = new StringReader(StageListData.text))
			{
				string line = null;
				while ((line = reader.ReadLine()) != null)
				{
					// format: "<path> <title>"
					var kv = line.Split(new char[]{ ' ' }, 2);
					StageList.Add(new StageInfo {
						Title = kv[1],
						Path = kv[0],
						Data = Resources.Load<TextAsset>(kv[0]),
					});
				}
			}
		}
		else
		{
			Debug.LogWarning("List not found");
		}
			
		var wrapTransform = (RectTransform)ButtonsWrap.transform;
		var wrapRect = wrapTransform.rect;
		float x = 0, y = 0, pageX = 0, pageWidth = wrapRect.width, pageHeight = wrapRect.height;
		float width = 0, height = 0, paddingX = 0, paddingY = 0;
		int index = 0;
		foreach (var stage in StageList)
		{
			var newButton = Instantiate(StageButtonProto);
			newButton.transform.SetParent(ButtonsWrap.transform);
			Field field = newButton.GetComponentInChildren<Field>();
			using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(stage.Data.text)))
			{
				using (var reader = new XmlTextReader(stream))
				{
					reader.ReadToFollowing("Stage");
					reader.ReadToDescendant("ObjectiveField");
					field.ReadXml(reader);
				}
			}
			var rectTransform = (RectTransform)newButton.transform;
			if (width == 0 && height == 0)
			{
				width = rectTransform.rect.width * 1.2f;
				height = (rectTransform.rect.height + ((RectTransform)newButton.transform.Find("Title").transform).rect.height) * 1.2f;
				paddingX = (width / 1.2f * 0.1f);
				x = paddingX;
				paddingY =(height / 1.2f * 0.1f) + rectTransform.rect.height;
				y = paddingY;
				float marginX = pageWidth % width, marginY = pageHeight % height;
				((RectTransform)ScrollView.transform).sizeDelta = new Vector2(-marginX, -marginY);
				wrapRect = wrapTransform.rect;
				pageWidth = wrapRect.width;
				pageHeight = wrapRect.height;
				Debug.LogFormat("{0} {1} {2} {3}", width, height, marginX, marginY);
			}

			newButton.name = index.ToString();
			newButton.transform.localPosition = new Vector3(x - (pageWidth / 2) + pageX, -y + pageHeight / 2, newButton.transform.localPosition.z);
			x += width;
			if (x + width / 2.0f > pageWidth)
			{
				x = paddingX;
				if (y + height / 2.0f > pageHeight)
				{
					y = paddingY;
					pageX += pageWidth / 2;
					wrapTransform.sizeDelta += new Vector2(pageWidth, 0);
				}
				else
				{
					y += height;
				}
			}
			index++;
			var title = newButton.transform.Find("Title").GetComponent<Text>();
			title.text = stage.Title;
			newButton.GetComponent<Button>().onClick.AddListener(this.StageButtonClicked);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StageButtonClicked()
	{
		Debug.Log("clicked");
		GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
		levelManager.NextLevel = StageList[int.Parse(clickedButton. name)].Data;
		DontDestroyOnLoad(levelManager);
		Application.LoadLevel(Constants.GameSceneName);
	}
}
