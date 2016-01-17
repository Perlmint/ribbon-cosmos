using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public int Size { get; set; }

	private Block[] World { get; set; }

	public Block block(int x, int y)
	{
		return World[y * Size + x];
	}

	public enum Direction
	{
		Horizontal,
		Vertical}

	;

	private delegate Block getter_type(int pos);

	public void ApplyRibbon(Direction direction, int pos, Ribbon ribbon)
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

	void Start()
	{
		Size = GameObject.Find("GameManager").GetComponent<MakeStage>().size;
		int size = Size;
		World = new Block[size * size];
	}
}
