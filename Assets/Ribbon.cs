using UnityEngine;
using System.Collections;

public class Ribbon : MonoBehaviour
{
	public Color color { get; private set; }
	public int width { get; private set; }
	public enum RibbonType {
		Additive, // 가산혼합
		Subtractive // 감산혼합
	};
	public RibbonType type { get; private set; }

	public Ribbon(Color color, int width, RibbonType ribbonType)
	{
		this.color = color;
		this.width = width;
		this.type = ribbonType;
	}

	public void ApplyColor(Color color)
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
}

