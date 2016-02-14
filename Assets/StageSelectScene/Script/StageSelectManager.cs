using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Common;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectManager : MonoBehaviour
{
	public List<KeyValuePair<string, string>> StageList { get; set; }
	public GameObject StageButtonProto;
	private StageLevelManager levelManager;
	public GameObject ButtonsWrap;
	public GameObject ScrollView;

	public StageSelectManager()
	{
		StageList = new List<KeyValuePair<string, string>>();
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
					StageList.Add(new KeyValuePair<string, string>(kv[0], kv[1]));
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
			title.text = stage.Value;
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
		levelManager.NextLevel = Resources.Load<TextAsset>(StageList[int.Parse(clickedButton. name)].Key);
		DontDestroyOnLoad(levelManager);
		Application.LoadLevel(Constants.GameSceneName);
	}
}
