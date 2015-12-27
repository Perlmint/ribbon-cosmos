using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public int Size { get; set; }
	private Block[] World { get; set; }

	public Stage(int size) {
		Size = size;
		World = new Block[size * size];
	}

	public Block block(int x, int y) {
		return World[y * Size + x];
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
