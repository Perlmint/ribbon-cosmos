using UnityEngine;
using System.Collections;

/// <summary>
/// Block on stage. default color is black.
/// </summary>
public class Block : MonoBehaviour
{
	private Color _color;
	public Color color { get { return _color; } private set { _color = value; } }

	public Block()
	{
		color = Color.black;
	}

	public void ApplyRibbon(Ribbon ribbon)
	{
		ribbon.ApplyColor(ref this._color);
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

