using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RibbonGameManager : MonoBehaviour {
    
    private static RibbonGameManager instance;
    public static RibbonGameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(RibbonGameManager)) as RibbonGameManager;
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
        RibbonGameStageData curstagedata = RibbonGameStageManager.Instance.GetCurrentStageData();
        curstagedata.ResetRibbon();
        GoalStageData = curstagedata.GoalData;
        CurData = new RibbonGameData();
        CurData.Init(GoalStageData.Width, GoalStageData.Height);
    }

    public RibbonGameData GoalStageData;
    public RibbonGameData CurData;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public bool CheckGameComplete()
    {
        return GoalStageData.IsSame(CurData);
    }
}

[System.Serializable]
public class RibbonData
{
    public int RibbonID;
    public int MaxCount;
    public int CurCount;
    public Color RibbonColor;
    public int Width;

    public RibbonData(int _id, int _maxcount, Color _color, int _width)
    {
        RibbonID = _id;
        MaxCount = _maxcount;
        CurCount = _maxcount;
        RibbonColor = _color;
        Width = _width;
    }
}

[System.Serializable]
public class RibbonGameStageData
{
    public int StageNumber;
    public RibbonGameData GoalData;
    public List<RibbonData> RibbonList = new List<RibbonData>();

    public void ResetRibbon()
    {
        for(int iter = 0; iter < RibbonList.Count; iter++)
        {
            RibbonList[iter].CurCount = RibbonList[iter].MaxCount;
        }
    }
}

[System.Serializable]
public class RibbonGameData
{
    public int Width;
    public int Height;

    public List<Color> ColorList;
    public List<bool> HorizontalColorAddChecker;
    public List<bool> VerticalColorAddChecker;

    public List<int> ColorCheckIndexList;
    public void Init(int _width, int _height)
    {
        Width = _width;
        Height = _height;
        ColorList = new List<Color>();
        for (int iter = 0; iter < _width * _height; iter++)
        {
            ColorList.Add(new Color(0f, 0f, 0f, 1f));
        }
        ColorCheckIndexList = new List<int>();
        VerticalColorAddChecker = new List<bool>();
        HorizontalColorAddChecker = new List<bool>();
        for (int xiter = 0; xiter < Width; xiter++)
        {
            VerticalColorAddChecker.Add(false);
        }
        for(int yiter = 0; yiter < Height; yiter++)
        {
            HorizontalColorAddChecker.Add(false);
        }
        
    }

    public bool IsSame(RibbonGameData _data)
    {
        for(int iter = 0; iter < ColorCheckIndexList.Count; iter++)
        {
            int curindex = ColorCheckIndexList[iter];
            Color curcolor = ColorList[curindex];
            Color othercolor = _data.ColorList[curindex];
            if(curcolor.r != othercolor.r 
                || curcolor.g != othercolor.g 
                || curcolor.b != othercolor.b)
            {
                return false;
            }
        }

        return true;
    }

    public bool CanAddColor(bool _horizontal, int _numb)
    {
        if(_horizontal)
        {
            if(_numb >= 0 && _numb < Height)
            {
                if(!HorizontalColorAddChecker[_numb])
                {
                    return true;
                }
            }
        }else
        {
            if (_numb >= 0 && _numb < Width)
            {
                if (!VerticalColorAddChecker[_numb])
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void AddColor(bool _horizontal, int _numb, Color _color)
    {
        if(_horizontal)
        {
            if(_numb >= 0 && _numb < Height)
            {
                HorizontalColorAddChecker[_numb] = true;
                for (int xiter = 0; xiter < Width; xiter++)
                {
                    int curiter = xiter + _numb * Width;
                    Color curcolor = ColorList[curiter];
                    curcolor += _color;
                    ColorList[curiter] = curcolor;
                }
            }
        }else
        {
            if (_numb >= 0 && _numb < Width)
            {
                VerticalColorAddChecker[_numb] = true;
                for (int yiter = 0; yiter < Height; yiter++)
                {
                    int curiter = yiter * Width + _numb;
                    Color curcolor = ColorList[curiter];
                    curcolor += _color;
                    ColorList[curiter] = curcolor;
                }
            }
        }
    }

    public void SetGoalColor(int _x, int _y, Color _color)
    {
        if (_x >= 0 && _x < Width && _y >= 0 && _y < Height)
        {
            int curindex = _x + _y * Width;
            ColorList[curindex] = _color;
            if (!ColorCheckIndexList.Contains(curindex))
            {
                ColorCheckIndexList.Add(curindex);
            }
        }
    }
}



