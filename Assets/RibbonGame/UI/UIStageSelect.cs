using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIStageSelect : MonoBehaviour {

    public Transform StageUIPivot;
    public List<UIStageObject> UIStageList = new List<UIStageObject>();
    public GameObject StageUIPrefab;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        for(int iter = 0; iter < RibbonGameStageManager.Instance.RibbonStageList.Count; iter++)
        {
            GameObject genobj = Instantiate(StageUIPrefab) as GameObject;
            UIStageObject genstage = genobj.GetComponent<UIStageObject>();

            genobj.transform.parent = StageUIPivot;
            genobj.gameObject.SetActive(true);
            genstage.Init(RibbonGameStageManager.Instance.RibbonStageList[iter]);
            UIStageList.Add(genstage);

            genstage.UpdateUI();
        }
    }
}
