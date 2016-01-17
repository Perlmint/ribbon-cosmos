using UnityEngine;
using System.Collections;

public class MakeStage : MonoBehaviour
{
	protected Stage curStage;
	public int size;

	void Start () {
		curStage = GameObject.FindObjectOfType<Stage>();
		curStage.Size = size;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.A))
			Temp ();
	}

	void Temp(){
		Ribbon temp;
		temp = new Ribbon(Color.red, 1, Ribbon.RibbonType.Additive);

		curStage.ApplyRibbon(Stage.Direction.Horizontal, 1, temp);
	}
}
