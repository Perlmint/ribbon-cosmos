using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGoalDisplay : MonoBehaviour {

    public RibbonGameData CurGoalData;
    
    public float TotalWidth = 400f;
    public float TotalHeight = 400f;
    public Transform BlockPivot;
    public GameObject UIBlockPrefab;
    public List<UIGameBlock> UIBlockList = new List<UIGameBlock>();

    public void Init(RibbonGameData _data)
    {
        CurGoalData = _data;

        int xpaddingnumb = _data.Width - 1;
        int ypaddingnumb = _data.Height - 1;
        float xpadding = TotalWidth / (float)_data.Width * 0.1f;
        float ypadding = TotalHeight / (float)_data.Height * 0.1f;
        float xsize = (TotalWidth - xpadding * xpaddingnumb) / (float)_data.Width;
        float ysize = (TotalHeight - ypadding * ypaddingnumb) / (float)_data.Height;

        float xstart = -xsize * ((float)_data.Width / 2f) - xpadding * (_data.Width / 2) + xsize / 2f;
        float ystart = -ysize * ((float)_data.Height / 2f) - ypadding * (_data.Height / 2) + ysize / 2f;

        for (int iter = 0; iter < _data.Width * _data.Height; iter++)
        {
            int curxindex = iter % _data.Width;
            int curyindex = iter / _data.Width;

            float xpos = xstart + curxindex * xpadding + curxindex * xsize;
            float ypos = ystart + curyindex * ypadding + curyindex * ysize;
            GameObject genobject = Instantiate(UIBlockPrefab) as GameObject;
            UIGameBlock genblock = genobject.GetComponent<UIGameBlock>();
            genobject.transform.parent = BlockPivot;
            genobject.gameObject.SetActive(true);

            UIBlockList.Add(genblock);

            genblock.Init(xsize, ysize);

            genobject.GetComponent<RectTransform>().localPosition = new Vector3(xpos, ypos, 0f);

        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int iter = 0; iter < CurGoalData.ColorList.Count; iter++)
        {
            UIGameBlock curblock = UIBlockList[iter];
            curblock.UpdateUI(CurGoalData.ColorList[iter]);
        }
    }
}
