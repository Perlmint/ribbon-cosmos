using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UISelectLine : MonoBehaviour {

    public RibbonGameUIManager GameUIManager;

    public bool Horizontal;
    public int LineIndex;

    public Image BackgroundImage;
    public void Init(bool _horizontal, int _line, float _imagewidth, float _imageheight)
    {
        Horizontal = _horizontal;
        LineIndex = _line;
        BackgroundImage.rectTransform.sizeDelta = new Vector2(_imagewidth,  _imageheight);
    }

    public void OnClickLine()
    {
        GameUIManager.OnClickSelectLine(this);
    }
}
