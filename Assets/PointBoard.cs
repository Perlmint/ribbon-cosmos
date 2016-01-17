using UnityEngine;
using System.Collections;
///<summary>
///Point_Sum_Board
///</summary>
public enum GameState
{

    Ready,
    Play,
    RePlay,
    End

}
//PointBoard는 다른 Block C# script에서 접근하기 위해 해당 스크립트에
//public PointBoard PB; 선언해주어야 접근가능.
public class PointBoard : MonoBehaviour {

    public GameState GS;            //게임매니저의 상태 관리
    public int stage_Goal[];
    public Block[] Blocks;          //블록 배열
    public int Count_Conjunction;   //혼합색이 일치하는 블록의 개수
    public int Count_Basic;         //기본 단색 블럭 맞는 경우
    public int Numstars;            //별 개수
    public Texture stars;           //별

    public GameObject FinishGUI;    //결과화면 보여주기 위한 오브젝트
    public GUIText Final_Score;     //결과화면에서 보여줄 총합 점수
    public GUIText Final_Count_Basic;
    public GUIText Final_Count_Conjunction;

    public AudioClip ReadySound;
    public AudioClip FinishSound;
    public AudioClip StartSound;
	// Use this for initialization
	void Start () {
      //  audio.clip = ReadySound;
     //   audio.Play();
	}
	public void GO()
    {
        GS = GameState.Play;
        //audio.clip = StartSound;
        //audio.Play();
    }
	// Update is called once per frame
	void Update () {
	if(GS == GameState.Play)
        {
            //스택상의 리본이 전부 소진된 경우
            //게임이 끝나는 시점이 되어야 함.
            End();
        }

	}
    void End()
    {
        GS = GameState.End;
        Final_Count_Basic.text = string.Format("{0}", Count_Basic);
        Final_Count_Conjunction.text = string.Format("{0}", Count_Conjunction);
        Final_Score.text = string.Format("{0}", Count_Basic * 10 + Count_Conjunction * 100);
       
        FinishGUI.gameObject.SetActive(true);

        //위의 합산 점수(score)에 따라 별표 계산..but 필요조건을 설정하는 것은 좀더 생각해봐야.
        if (Count_Conjunction < stage_Goal * 0.75)
        {
            Numstars = 1;
        }
        else if (Count_Conjunction < stage_Goal * 0.75) {
            Numstars = 2;
        }
        else if (Count_Conjunction == stage_Goal)
        {
            Numstars = 3;
        }

    }
}
