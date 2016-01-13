using UnityEngine;
using System.Collections;

public class EachBlock : MonoBehaviour {
	public int x;
	public int y;
	public Block cBlock;

	void Start()
	{
		cBlock = new Block();
	}
}
