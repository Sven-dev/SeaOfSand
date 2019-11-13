using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintState : State
{
    public BlockPainter BlockPainter;
    public DiagonalCursor Cursor;
    public RecolourManager RecolourManager;

    public override void Enable()
    {
        base.Enable();

        Joycons.OnLeftStick += Cursor.Move;
        Joycons.OnA += BlockPainter.Paint;
        Joycons.OnY += RecolourManager.ChangeColour;
    }

    public override void Disable()
    {
        base.Disable();

        Joycons.OnLeftStick -= Cursor.Move;
        Joycons.OnA -= BlockPainter.Paint;
        Joycons.OnY += RecolourManager.ChangeColour;
    }
}