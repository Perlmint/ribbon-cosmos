using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Field : MonoBehaviour, IXmlSerializable
{
	public GameObject blockProto;
	public float Padding;
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

			if (size == 0)
			{
				World = new Block[0];
				return;
			}

			// 이전에 생성된 블럭을 전부 제거한다.
			foreach (GameObject obj in blockObjs)
			{
				Destroy(obj);
			}
			var fieldBound = GetComponent<Renderer>().bounds;
			var fieldSize = fieldBound.size;
			var fieldCenter = fieldBound.center;
			var blockSize = blockProto.GetComponent<Renderer>().bounds.size;
			float padding = 1.0f - Padding;
			// 어차피 transform.parent를 지정하면 local좌표계가 parent에 상대적인 좌표계가 된다.
			// 그러니 그냥 0.0f ~ 1.0f을 기준으로 위치 및 스케일을 구하면 된다.
			Vector3 blockScale = new Vector3(1.0f / size * padding, 1.0f / size * padding, 1.0f);
			Vector3 blockPositionUnit = new Vector3(1.0f / size, 1.0f / size, 1);

			World = new Block[size * size];
			for (int x = 0; x < size; x++) {
				for (int y = 0; y < size; y++) {
					GameObject curBlock;
					curBlock = Instantiate(blockProto) as GameObject;
					curBlock.transform.parent = this.transform;
					var position = new Vector3(x - ((size - 1) / 2), y - ((size - 1) / 2), -1);
					position.Scale(blockPositionUnit);
					curBlock.transform.localPosition = position;
					curBlock.transform.localScale = blockScale;
					setBlock(x, y, curBlock.GetComponent<Block>());
					blockObjs.Add(curBlock);
				}
			}
		}
	}

	private List<GameObject> blockObjs = new List<GameObject>();

	private Block[] World { get; set; }

	public Block block(int x, int y)
	{
		return World[y * Size + x];
	}

	public Vector2 InputTest(Ray ray)
	{
		for (int i = 0; i < Size; i++)
		{
			for (int j = 0; j < Size; j++)
			{
				RaycastHit hit;
				if (block(i, j).GetComponent<Collider>().Raycast(ray, out hit, float.PositiveInfinity))
				{
					return new Vector2(i, j);
				}
			}
		}
		return new Vector2(float.NaN, float.NaN);
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

	#region IXmlSerializable implementation


	public System.Xml.Schema.XmlSchema GetSchema()
	{
		return null;
	}


	public void ReadXml(System.Xml.XmlReader reader)
	{
		Size = int.Parse(reader.GetAttribute("Size"));
		for (int x = 0; x < size; x++)
		{
			reader.ReadToFollowing("Row");
			for (int y = 0; y < size; y++)
			{
				var currentBlock = block(x, y);
				reader.ReadToFollowing("Block");
				currentBlock.ReadXml(reader);
			}
		}
	}


	public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString("Size", Size.ToString());
		for (int x = 0; x < size; x++)
		{
			writer.WriteStartElement("Row");
			for (int y = 0; y < size; y++)
			{
				var currentBlock = block(x, y);
				writer.WriteStartElement("Block");
				currentBlock.WriteXml(writer);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}
	}


	#endregion

}
