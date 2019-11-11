using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourPicker : MonoBehaviour {

    public Material material;

    public void Click()
    {
        BlockPainter.SetColour(material);
    }
}
