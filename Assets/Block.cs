using UnityEngine;
using System.Collections;

/// <summary>
/// Block on stage. default color is black.
/// </summary>
public class Block : MonoBehaviour
{
	private Color _color;

	public Color color {
		get { return _color; }
		private set {
			_color = value;
			GetComponent<Renderer>().material.color = _color;
		}
	}

	public Block()
	{
		color = Color.black;
	}

	public void ApplyRibbon(Ribbon ribbon)
	{
		ribbon.ApplyColor(this.color);
	}
}

