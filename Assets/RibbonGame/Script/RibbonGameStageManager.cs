using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RibbonGameStageManager : MonoBehaviour {

    private static RibbonGameStageManager instance;
    public static RibbonGameStageManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType(typeof(RibbonGameStageManager)) as RibbonGameStageManager;                
                instance.Initiate();
            }

            return instance;
        }
    }

    void OnDestroy()
    {
        instance = null;
    }

    private void Initiate()
    {
        DontDestroyOnLoad(gameObject);
        TempInitData();
    }

    public int SelectedStageNumber;
    public List<RibbonGameStageData> RibbonStageList = new List<RibbonGameStageData>();

    private void TempInitData()
    {
        RibbonGameStageData stage1 = new RibbonGameStageData();
        stage1.StageNumber = 1;
        RibbonGameData goal1 = new RibbonGameData();
        goal1.Init(5, 5);
        goal1.SetGoalColor(0, 4, Color.red);
        goal1.SetGoalColor(1, 3, Color.blue);
        goal1.SetGoalColor(2, 2, Color.red);
        goal1.SetGoalColor(3, 1, Color.blue);
        goal1.SetGoalColor(4, 0, Color.red);
        stage1.GoalData = goal1;
        List<RibbonData> ribbonlist1 = new List<RibbonData>();
        ribbonlist1.Add(new RibbonData(0, 3, Color.red, 1));
        ribbonlist1.Add(new RibbonData(1, 2, Color.blue, 1));
        stage1.RibbonList = ribbonlist1;
        RibbonStageList.Add(stage1);

        RibbonGameStageData stage2 = new RibbonGameStageData();
        stage2.StageNumber = 2;
        RibbonGameData goal2 = new RibbonGameData();
        goal2.Init(5, 5);
        goal2.SetGoalColor(0, 0, Color.red);
        goal2.SetGoalColor(4, 4, Color.blue);
        goal2.SetGoalColor(0, 4, Color.red + Color.blue);
        stage2.GoalData = goal2;
        List<RibbonData> ribbonlist2 = new List<RibbonData>();
        ribbonlist2.Add(new RibbonData(0, 1, Color.red, 1));
        ribbonlist2.Add(new RibbonData(1, 1, Color.blue, 1));
        stage2.RibbonList = ribbonlist2;
        RibbonStageList.Add(stage2);

        RibbonGameStageData stage3 = new RibbonGameStageData();
        stage3.StageNumber = 3;
        RibbonGameData goal3 = new RibbonGameData();
        goal3.Init(5, 5);
        goal3.SetGoalColor(0, 4, Color.red + Color.blue);
        goal3.SetGoalColor(1, 3, Color.red);
        goal3.SetGoalColor(2, 2, Color.red + Color.blue);
        goal3.SetGoalColor(3, 1, Color.blue);
        goal3.SetGoalColor(4, 0, Color.red + Color.blue);
        stage3.GoalData = goal3;
        List<RibbonData> ribbonlist3 = new List<RibbonData>();
        ribbonlist3.Add(new RibbonData(0, 4, Color.red, 1));
        ribbonlist3.Add(new RibbonData(1, 4, Color.blue, 1));
        stage3.RibbonList = ribbonlist3;
        RibbonStageList.Add(stage3);
    }

	// Use this for initialization
	void Start () {
        if(RibbonGameStageManager.Instance == null);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public RibbonGameStageData GetCurrentStageData()
    {
        //RibbonGameStageData curdata = null;
        for(int iter = 0; iter < RibbonStageList.Count; iter++)
        {
            if(RibbonStageList[iter].StageNumber == SelectedStageNumber)
            {
                return RibbonStageList[iter];
            }
        }

        return null;
    }

    public bool HasStage(int _index)
    {
        for(int iter = 0; iter < RibbonStageList.Count; iter++)
        {
            if(RibbonStageList[iter].StageNumber == _index)
            {
                return true;
            }
        }
        return false;
    }
}
