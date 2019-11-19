using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecolourManager : MonoBehaviour
{
    public BlockPainter BlockPainter;

    public List<Colour> Colours;
    private int ColourIndex = 0;
    private int HueIndex = 0;

    public delegate void ColourChange(Color color);
    public static ColourChange OnColourChange;

    private void Start()
    {
        BlockPainter.SetColour(Colours[ColourIndex].Hues[HueIndex]);
    }

    public void ChangeColour()
    {
        ColourIndex++;
        if (ColourIndex > Colours.Count - 1)
        {
            ColourIndex = 0;
        }

        HueIndex = 0;
        BlockPainter.SetColour(Colours[ColourIndex].Hues[HueIndex]);
        OnColourChange(Colours[ColourIndex].Hues[HueIndex]);
    }

    public void ChangeHue()
    {
        HueIndex++;
        if (HueIndex > Colours[ColourIndex].Hues.Count - 1)
        {
            HueIndex = 0;
        }

        BlockPainter.SetColour(Colours[ColourIndex].Hues[HueIndex]);
        OnColourChange(Colours[ColourIndex].Hues[HueIndex]);
    }
}