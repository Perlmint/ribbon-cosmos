using UnityEngine;
using System.Collections;

public class MakeStage : MonoBehaviour {

	public GameObject initBlock;

	public float initBlockXpos;
	public float initBlockYpos;
	public float blockInterval;
	public Stage curStage;
	public int size;
	
	public void setStage(){
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
				GameObject curBlock;
				curBlock = Instantiate (initBlock,
					new Vector3(initBlockXpos + blockInterval * x, initBlockYpos + blockInterval * y, 0.0f), 
					this.transform.rotation) as GameObject;
				Debug.Log("create!\n");
				curBlock.GetComponent<EachBlock>().cBlock = curStage.block(x, y);
				curBlock.GetComponent<EachBlock>().x = x;
				curBlock.GetComponent<EachBlock>().y = y;
				Debug.Log("clear!\n");
			}
		}
	}

	void Start () {
		setStage ();
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.A))
			Temp ();
	}

	void Temp(){
		Ribbon temp;
		temp = new Ribbon(Color.red, 1, Ribbon.RibbonType.Additive);

		curStage.ApplyRibbon (Stage.Direction.Horizontal, 1, temp);
	}
}
