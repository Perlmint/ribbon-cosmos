using UnityEngine;
using System.Collections;

/// <summary>
/// Block on stage. default color is black.
/// </summary>
public class Block : MonoBehaviour
{
    public int x;
    public int y;
	protected Color _color;

	public Color color {
		get { return _color; }
		set { _color = value; }
	}

	public Block()
	{
		color = Color.black;
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
        gameObject.GetComponentInParent<TouchInput>().startX = x;
        gameObject.GetComponentInParent<TouchInput>().startY = y;
    }
}


