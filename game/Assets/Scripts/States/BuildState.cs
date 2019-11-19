using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : State
{
    public BlockPlacer BlockPlacer;

    public DiagonalCursor Cursor;
    public ColourManager ColourManager;
    public GameObject UI;

    public override void Enable()
    {
        base.Enable();

        Joycons.OnA += BlockPlacer.Place;
        Joycons.OnY += ColourManager.ChangeColour;
        Joycons.OnX += ColourManager.ChangeHue;

        Joycons.OnLeftStick += Cursor.Move;

        BlockPlacer.Toggle();
        UI.SetActive(true);
    }

    public override void Disable()
    {
        base.Disable();

        Joycons.OnA -= BlockPlacer.Place;
        Joycons.OnY -= ColourManager.ChangeColour;
        Joycons.OnX -= ColourManager.ChangeHue;

        Joycons.OnLeftStick -= Cursor.Move;

        BlockPlacer.Toggle();
        UI.SetActive(false);
    }
}