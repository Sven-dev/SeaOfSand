using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolourManager : MonoBehaviour {

    public BlockPainter BlockPainter;

    public List<Color> Colours;
    [HideInInspector]
    public int Index = 0;

    private void Start()
    {
        BlockPainter.SetColour(Colours[Index]);
    }

    public void ChangeColour()
    {
        Index++;
        if (Index > Colours.Count - 1)
        {
            Index = 0;
        }

        BlockPainter.SetColour(Colours[Index]);
    }
}