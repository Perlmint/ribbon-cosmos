using UnityEngine;
using System.Collections;

public class ObjBlock : MonoBehaviour {
    protected Color _color;

    public Color color
    {
        get { return _color; }
        set { _color = value; }
    }

    public ObjBlock()
    {
        color = Color.black;
    }
}
