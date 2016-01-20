using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIRibbonSelect : MonoBehaviour {

    public RibbonGameUIManager UIManager;
    public RibbonData MyRibbonData;
    public Image RibbonImage;
    public Text RibbonCountText;

    public bool Selected = false;
    public Image CurSelectedImage;

    public void Init(RibbonData _data)
    {
        MyRibbonData = _data;
        RibbonImage.color = _data.RibbonColor;
        RibbonCountText.text = _data.CurCount.ToString();
        SetSelected(false);
    }


    public void UpdateUI()
    {
        RibbonCountText.text = MyRibbonData.CurCount.ToString();
        if(MyRibbonData.CurCount == 0)
        {
            SetSelected(false);
        }
    }

    public void OnClick()
    {
        UIManager.OnClickRibbonItem(this);
    }

    public void SetSelected(bool _flag)
    {
        Selected = _flag;
        CurSelectedImage.gameObject.SetActive(Selected);
    }
}
