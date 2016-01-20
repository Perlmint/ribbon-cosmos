using UnityEngine;
using System.Collections;

public class TouchInput : MonoBehaviour {

    public Ribbon selected;

    public int startX;
    public int startY;
    public int endX;
    public int endY;
    public bool twoTime;

    Ribbon temp1, temp2, temp3;

    void CheckFirstInput()
    {
        if(startX != -1 && startY != -1)
        {
            endX = startX;
            endY = startY;
            startX = -1;
            startY = -1;
            twoTime = false;
        }
    }

    void CheckSecondInput()
    {
        if(startX != -1 && startY != -1)
        {
            if (startX == endX && startY != endY)
            {
                this.GetComponent<Stage>().ApplyRibbon(Stage.Direction.Vertical, startX, selected);
            }
            else if(startX != endX && startY == endY)
            {
                this.GetComponent<Stage>().ApplyRibbon(Stage.Direction.Horizontal, startY, selected);
            }

            twoTime = true;
            startX = -1;
            startY = -1;
            endX = -1;
            endY = -1;
        }
    }

	void Start () {
        startX = -1;
        startY = -1;
        endX = -1;
        endY = -1;
        twoTime = true;

        temp1 = new Ribbon(Color.red, 1, Ribbon.RibbonType.Additive);
        temp2 = new Ribbon(Color.blue, 2, Ribbon.RibbonType.Additive);
        temp3 = new Ribbon(Color.white, 1, Ribbon.RibbonType.Additive);
        selected = temp1;
    }
	
	// Update is called once per frame
	void Update () {
        if (twoTime)
        {
            CheckFirstInput();
        }
        else if (!twoTime)
        {
            CheckSecondInput();
        }

        if (Input.GetKeyDown(KeyCode.Z))
            selected = temp1;
        if (Input.GetKeyDown(KeyCode.X))
            selected = temp2;
        if (Input.GetKeyDown(KeyCode.C))
            selected = temp3;

    }
}
