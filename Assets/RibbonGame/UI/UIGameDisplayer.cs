using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIGameDisplayer : MonoBehaviour {

    public RibbonGameData GameData;

    public float TotalWidth = 400f;
    public float TotalHeight = 400f;
    public Transform BlockPivot;
    public GameObject UIBlockPrefab;
    public List<UIGameBlock> UIBlockList = new List<UIGameBlock>();

    public GameObject UISelectLinePrefab;
    public List<UISelectLine> HorizontalSelectList = new List<UISelectLine>();
    public List<UISelectLine> VerticalSelectList = new List<UISelectLine>();
    public void Init(RibbonGameData _data)
    {
        GameData = _data;

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

        for(int xiter = 0; xiter < _data.Width; xiter++)
        {
            float xpos = xstart + xiter * xpadding + xiter * xsize;
            GameObject genobject = Instantiate(UISelectLinePrefab) as GameObject;
            UISelectLine genline = genobject.GetComponent<UISelectLine>();
            genline.transform.parent = BlockPivot;
            genobject.gameObject.SetActive(true);

            VerticalSelectList.Add(genline);
            genline.Init(false, xiter, xsize, ysize);
            genobject.transform.localPosition = new Vector3(xpos, ystart - ysize - ypadding, 0f);
        }

        for (int yiter = 0; yiter < _data.Height; yiter++)
        {
            float ypos = ystart + yiter * xpadding + yiter * xsize;
            GameObject genobject = Instantiate(UISelectLinePrefab) as GameObject;
            UISelectLine genline = genobject.GetComponent<UISelectLine>();
            genline.transform.parent = BlockPivot;
            genobject.gameObject.SetActive(true);

            HorizontalSelectList.Add(genline);
            genline.Init(true, yiter, xsize, ysize);
            genobject.transform.localPosition = new Vector3(TotalWidth / 2f + xsize / 2f + xpadding, ypos, 0f);
            genobject.transform.localEulerAngles = new Vector3(0f, 0f, 90f);
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        for(int iter = 0; iter < GameData.ColorList.Count; iter++)
        {
            UIGameBlock curblock = UIBlockList[iter];
            curblock.UpdateUI(GameData.ColorList[iter]);
        }
    }
}
