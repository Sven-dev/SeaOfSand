using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourUI : MonoBehaviour
{
    private Image ColourImage;

	// Use this for initialization
	void Start ()
    {
        ColourImage = GetComponent<Image>();
        ColourManager.OnColourChange += UpdateColour;
        RecolourManager.OnColourChange += UpdateColour;
    }

    public void UpdateColour(Color color)
    {
        color.a = 1;
        ColourImage.color = color;
    }
}