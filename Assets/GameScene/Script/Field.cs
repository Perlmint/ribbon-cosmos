using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.Collections.Generic;

public class Field : MonoBehaviour, IXmlSerializable
{
	private GameObject _blockProto;
	private GameObject blockProto {
		get {
			return _blockProto ?? (_blockProto = StageManager.Current.blockProto);
		}
	}
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
			float padding = 1.0f - StageManager.Current.padding;
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

    public float xMouseDown, yMouseDown, xMouseUp, yMouseUp;

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

    // 임시 테스트용
    Ribbon temp;

    // block들의 collider를 꺼서 Field애 대한 클릭을 인식하게 함
    void CheckInputToApply(float x1, float y1, float x2, float y2)
    {
        float stdLength = 225.0f / size;

        x1 -= 340;
        x2 -= 340;
        y1 -= 105;
        y2 -= 105;

        int _x1 = (int)(x1 / stdLength);
        int _x2 = (int)(x2 / stdLength);
        int _y1 = (int)(y1 / stdLength);
        int _y2 = (int)(y2 / stdLength);

        if(_y1 < 0 || _y1 >= size || _y2 < 0 || _y2 >= size)
        {
            return;
            
        }
        else if (_x1 == _x2 && _y1 != _y2)
        {
            ApplyRibbon(Direction.Vertical, _x1, temp);
        }
        else if(_x1 != _x2 && _y1 == _y2)
        {
            ApplyRibbon(Direction.Horizontal, _y1, temp);
        }
        else
        {
            return;
        }

        Debug.Log("x1 x2 y1 y2 " + _x1 + " " + _x2 + " " + _y1 + " " + _y2);

    }

	void Start()
	{
		// init world;
		Size = size;

        // 임시 테스트용 리본, 후에 삭제
        temp = new Ribbon(Color.red, 1, Ribbon.RibbonType.Covering);
    }

    void OnMouseDown()
    {
        xMouseDown = Input.mousePosition.x;
        yMouseDown = Input.mousePosition.y;
    }

    void OnMouseUp()
    {
        xMouseUp = Input.mousePosition.x;
        yMouseUp = Input.mousePosition.y;

        CheckInputToApply(xMouseDown, yMouseDown, xMouseUp, yMouseUp);
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
