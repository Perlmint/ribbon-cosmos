using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIStageObject : MonoBehaviour {

    public RibbonGameStageData CurStageData;

    public Text StageNumber;
    public Image MyImage;

    public void Init(RibbonGameStageData _data)
    {
        CurStageData = _data;
        UpdateUI();
    }

    public void UpdateUI()
    {
        StageNumber.text = CurStageData.StageNumber.ToString();
        
    }

    public void OnClickButton()
    {
        RibbonGameStageManager.Instance.SelectedStageNumber = CurStageData.StageNumber;
        Application.LoadLevel(Constant.Scene_GamePlay);
    }
}
