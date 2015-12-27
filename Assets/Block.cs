using UnityEngine;
using System.Collections;

/// <summary>
/// Block on stage. default color is black.
/// </summary>
public class Block : MonoBehaviour
{
	public Color color { get; private set; }

	public Block()
	{
		color = Color.black;
	}

	public void ApplyRibbon(Ribbon ribbon)
	{
		ribbon.ApplyColor(this.color);
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

