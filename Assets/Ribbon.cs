using UnityEngine;
using System.Collections;

public class Ribbon : MonoBehaviour
{
	public Color color { get; private set; }
	public uint width { get; private set; }
	public enum RibbonType {
		Additive, // 가산혼합
		Subtractive // 감산혼합
	};
	public RibbonType type { get; private set; }

	public Ribbon(Color color, uint width)
	{
		this.color = color;
		this.width = width;
		this.type = RibbonType.Additive;
	}

	public void ApplyColor(ref Color color)
	{
		switch (type) {
		case RibbonType.Additive:
			color.r += this.color.r;
			color.g += this.color.g;
			color.b += this.color.b;
			break;
		case RibbonType.Subtractive:
			break;
		}
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

