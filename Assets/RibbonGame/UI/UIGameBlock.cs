using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIGameBlock : MonoBehaviour {

    public Image CurImage;

    public void Init(float _widthsize, float _heightsize)
    {
        CurImage.rectTransform.sizeDelta = new Vector2(_widthsize, _heightsize);
    }

    public void UpdateUI(Color _color)
    {
        CurImage.color = _color;
    }
}
