using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RibbonGameUIManager : MonoBehaviour {

    public RibbonGameStageData StageData;

    public Transform RibbonUISelectListPivot;
    public GameObject RibbonUISelectPrefab;
    public List<UIRibbonSelect> RibbonUISelectList = new List<UIRibbonSelect>();
    public UIGoalDisplay GoalDisplay;
    public UIGameDisplayer GameDisplayer;
    
    private RibbonData CurSelectedRibbon;

    public GameObject CompleteUI;
    public GameObject NextStageButton;
    // Use this for initialization
	void Start () {
        if(RibbonGameManager.Instance == null)
        {

        };
        Init(RibbonGameStageManager.Instance.GetCurrentStageData());

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(RibbonGameStageData _data)
    {
        StageData = _data;
        for(int iter = 0; iter < _data.RibbonList.Count; iter++)
        {
            GameObject genobject = Instantiate(RibbonUISelectPrefab) as GameObject;
            UIRibbonSelect genribbonselect = genobject.GetComponent<UIRibbonSelect>();
            genribbonselect.Init(_data.RibbonList[iter]);
            genobject.transform.parent = RibbonUISelectListPivot;
            genobject.gameObject.SetActive(true);
            RibbonUISelectList.Add(genribbonselect);
        }
        GoalDisplay.Init(StageData.GoalData);
        GameDisplayer.Init(RibbonGameManager.Instance.CurData);
                
        if(RibbonGameStageManager.Instance.HasStage(RibbonGameStageManager.Instance.SelectedStageNumber + 1))
        {
            NextStageButton.gameObject.SetActive(true);
        }else
        {
            NextStageButton.gameObject.SetActive(false);
        }

        CompleteUI.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        for(int iter = 0; iter < RibbonUISelectList.Count; iter++)
        {
            RibbonUISelectList[iter].UpdateUI();
        }
        GameDisplayer.UpdateUI();
    }

    public void RestartGame()
    {

    }

    public void CheckGameComplete()
    {
        if(RibbonGameManager.Instance.CheckGameComplete())
        {
            Debug.Log("Game Complete");
            CompleteUI.gameObject.SetActive(true);
        }
    }

    public void OnClickRibbonItem(UIRibbonSelect _select)
    {
        for(int iter = 0; iter < RibbonUISelectList.Count; iter++)
        {
            UIRibbonSelect curribbonselect = RibbonUISelectList[iter];
            if(curribbonselect == _select)
            {
                if (_select.MyRibbonData.CurCount > 0)
                {
                    curribbonselect.SetSelected(true);
                    CurSelectedRibbon = curribbonselect.MyRibbonData;
                }
            }
            else
            {
                curribbonselect.SetSelected(false);
            }
        }
    }

    public void OnClickSelectLine(UISelectLine _line)
    {
        if(CurSelectedRibbon == null)
        {
            return;
        }

        //reduce line data. and add color and update.
        if (RibbonGameManager.Instance.CurData.CanAddColor(_line.Horizontal, _line.LineIndex))
        {
            RibbonGameManager.Instance.CurData.AddColor(_line.Horizontal, _line.LineIndex, CurSelectedRibbon.RibbonColor);
            CurSelectedRibbon.CurCount--;
            if (CurSelectedRibbon.CurCount == 0)
            {
                CurSelectedRibbon = null;
            }

            UpdateUI();

            //check game end
            CheckGameComplete();
        }
    }

    public void OnClickToMenu()
    {
        Application.LoadLevel(Constant.Scene_Stage);
    }

    public void OnClickRetry()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void OnClickToNext()
    {
        RibbonGameStageManager.Instance.SelectedStageNumber++;
        Application.LoadLevel(Application.loadedLevel);
    }
}
