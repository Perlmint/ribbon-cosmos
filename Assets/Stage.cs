using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public int Size { get; private set; }
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

	private delegate Block getter_type(uint pos);

	public void ApplyRibbon(Direction direction, uint pos, Ribbon ribbon)
	{
		getter_type getter;
		if (direction == Direction.Horizontal)
		{
			getter = (uint p) => {
				return this.block(p, pos);
			};
		}
		else
		{
			getter = (uint p) => {
				return this.block(pos, p);
			};
		}

		for (uint i = 0; i < Size; ++i)
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
