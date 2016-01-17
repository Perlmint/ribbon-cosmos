﻿using UnityEngine;
using System.Collections;

public class Stage : MonoBehaviour {
	public GameObject blockProto;
	private int size;

	public int Size { 
		get { return size; }
		set {
			size = value;
			if (size == 0)
			{
				World = new Block[0];
				return;
			}

			var fieldBound = GetComponent<Renderer>().bounds;
			var fieldSize = fieldBound.size;
			var fieldCenter = fieldBound.center;
			float width = fieldSize.x, height = fieldSize.y;
			float deltaX = width / size, deltaY = height / size;
			float beginX = fieldCenter.x - width / 2, beginY = fieldCenter.y - height / 2;

			var blockSize = blockProto.GetComponent<Renderer>().bounds.size;
			World = new Block[size * size];
			for (int x = 0; x < size; x++) {
				for (int y = 0; y < size; y++) {
					GameObject curBlock;
					curBlock = Instantiate (blockProto,
						new Vector3(beginX + deltaX * x + blockSize.x / 2, beginY + deltaY* y + blockSize.y / 2, 0.0f), 
						this.transform.rotation) as GameObject;
					curBlock.transform.parent = this.transform;
					setBlock(x, y, curBlock.GetComponent<Block>());
				}
			}
		}
	}

	private Block[] World { get; set; }

	public Block block(int x, int y)
	{
		return World[y * Size + x];
	}

	public void setBlock(int x, int y, Block block)
	{
		World[y * Size + x] = block;
	}

	public enum Direction
	{
		Horizontal,
		Vertical
	};

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
		// init world;
		Size = size;
	}
}
