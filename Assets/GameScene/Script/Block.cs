using UnityEngine;
using System.Collections;
using System.Xml.Serialization;

/// <summary>
/// Block on stage. default color is black.
/// </summary>
public class Block : MonoBehaviour, IXmlSerializable
{
    public int x, y;
	protected Color _color;

    float firstPosX, firstPosY, secondPosX, secondPosY;
    int sizeOfStage;
    Ribbon temp;

    public Color color {
		get { return _color; }
		set { _color = value; }
	}

	public Block()
	{
		color = Color.black;
        x = -1;
        y = -1;
        sizeOfStage = -1;
	}

	public void ApplyRibbon(Ribbon ribbon)
	{
		ribbon.ApplyColor(ref this._color);
	}

	public void Update()
	{
		GetComponent<Renderer>().material.color = _color;
	}

    void OnMouseDown()
    {
        firstPosX = Input.mousePosition.x;
        firstPosY = Input.mousePosition.y;
    }

    void OnMouseUp()
    {
        secondPosX = Input.mousePosition.x;
        secondPosY = Input.mousePosition.y;
        CheckMouseInput();
    }

    void CheckMouseInput()
    {
        float xDiff = secondPosX - firstPosX;
        float yDiff = secondPosY - firstPosY;

        if (xDiff > 0 && Mathf.Abs(yDiff) < xDiff && x != sizeOfStage-1)
        {
            GameObject.Find("Field").GetComponent<Field>().ApplyRibbon(Field.Direction.Horizontal, y, temp);
            Debug.Log("1");
        }
        if (xDiff < 0 && Mathf.Abs(yDiff) < xDiff * (-1f) && x != 0)
        {
            GameObject.Find("Field").GetComponent<Field>().ApplyRibbon(Field.Direction.Horizontal, y, temp);
            Debug.Log("2");
        }
        if (yDiff > 0 && Mathf.Abs(xDiff) < yDiff && y != sizeOfStage-1)
        {
            GameObject.Find("Field").GetComponent<Field>().ApplyRibbon(Field.Direction.Vertical, x, temp);
            Debug.Log("3");
        }
        if(yDiff < 0 && Mathf.Abs(xDiff) < yDiff * (-1f) && y != 0)
        {
            GameObject.Find("Field").GetComponent<Field>().ApplyRibbon(Field.Direction.Vertical, x, temp);
            Debug.Log("4");
        }


    }

    void Start()
    {
        sizeOfStage = GameObject.Find("GameManager").GetComponent<StageManager>().size;
        temp = new Ribbon(Color.red, 1, Ribbon.RibbonType.Additive);
    }

	#region IXmlSerializable implementation

	public System.Xml.Schema.XmlSchema GetSchema()
	{
		return null;
	}

	public void ReadXml(System.Xml.XmlReader reader)
	{
		color = new Color(
			float.Parse(reader.GetAttribute("r")),
			float.Parse(reader.GetAttribute("g")),
			float.Parse(reader.GetAttribute("b")));
	}

	public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString("r", color.r.ToString());
		writer.WriteAttributeString("g", color.g.ToString());
		writer.WriteAttributeString("b", color.b.ToString());
	}

	#endregion
}