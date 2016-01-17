using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour
{
	protected Field currentField;
	public int size;

	void Start () {
		currentField = GameObject.FindObjectOfType<Field>();
		currentField.Size = size;
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.A))
			Temp ();
	}

	void Temp(){
		Ribbon temp;
		temp = new Ribbon(Color.red, 1, Ribbon.RibbonType.Additive);

		currentField.ApplyRibbon(Field.Direction.Horizontal, 1, temp);
	}
}
