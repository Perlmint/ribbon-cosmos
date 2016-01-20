using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIInitScene : MonoBehaviour {

    public Image Logo;

    public float SceneChange;
    public float AlphaSpeed;
    void Start()
    {
        Color curcolor = Logo.color;
        curcolor.a = 0f;
        Logo.color = curcolor;
    }

    void Update()
    {
        Process(Time.deltaTime);
    }

    public void Process(float _delta)
    {
        Color curcolor = Logo.color;
        curcolor.a += _delta * AlphaSpeed;
        Logo.color = curcolor;

        SceneChange -= _delta;
        if(SceneChange <= 0f)
        {
            Application.LoadLevel(Constant.Scene_Stage);
        }
    }
}
