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

	public Block block(int x, int y)
	{
		return World[y * Size + x];
	}

	public enum Direction
	{
		Horizontal,
		Vertical
	};

	private delegate Block getter_type(int pos);

	public void ApplyRibbon(Direction direction, int pos, ref Ribbon ribbon)
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
			getter(i).ApplyRibbon(ref ribbon);
		}
	}

	public Stage stage1;

	// Use this for initialization
	void Start()
	{
		stage1 = new Stage(this.gameObject.GetComponent<MakeStage>().size);
		Debug.Log(stage1.Size);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
