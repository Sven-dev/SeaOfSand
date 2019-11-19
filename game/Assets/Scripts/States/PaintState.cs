using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintState : State
{
    public BlockPainter BlockPainter;
    public DiagonalCursor Cursor;
    public ColourManager ColourManager;
    public GameObject UI;

    public override void Enable()
    {
        base.Enable();

        Joycons.OnLeftStick += Cursor.Move;
        Joycons.OnA += BlockPainter.Paint;
        Joycons.OnY += ColourManager.ChangeColour;
        Joycons.OnX += ColourManager.ChangeHue;

        BlockPainter.Toggle();
        UI.SetActive(true);
    }

    public override void Disable()
    {
        base.Disable();

        Joycons.OnLeftStick -= Cursor.Move;
        Joycons.OnA -= BlockPainter.Paint;
        Joycons.OnY -= ColourManager.ChangeColour;
        Joycons.OnX -= ColourManager.ChangeHue;

        BlockPainter.Toggle();
        UI.SetActive(false);
    }
}