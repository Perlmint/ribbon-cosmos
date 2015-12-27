using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public int Size { get; set; }
	private Block[] World { get; set; }

	public Stage(int size)
	{
		Size = size;
		World = new Block[size * size];
	}

	public Block block(uint x, uint y)
	{
		return World[y * Size + x];
	}

	public enum Direction {
		Horizontal,
		Vertical
	};

	private delegate Block getter_type(int pos);

	public void ApplyRibbon(Direction direction, uint pos, Ribbon ribbon)
	{
		getter_type getter;
		if (direction == Direction.Horizontal)
		{
			getter = (int p) => {
				return this.block(p, pos);
			};
		}
		else
		{
			getter = (int p) => {
				return this.block(pos, p);
			};
		}

		for (int i = 0; i < Size; ++i)
		{
			getter(i).ApplyRibbon(ribbon);
		}
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
