using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildState : State
{
    public BlockPlacer BlockPlacer;
    public DiagonalCursor Cursor;

    public override void Enable()
    {
        Joycons.OnA += BlockPlacer.Place;
        Joycons.OnB += BlockPlacer.Undo;
        Joycons.OnY += BlockPlacer.Rotate;

        Joycons.OnLeftStick += Cursor.Move;

        BlockPlacer.Toggle();
        base.Enable();
    }

    public override void Disable()
    {
        Joycons.OnA -= BlockPlacer.Place;
        Joycons.OnB -= BlockPlacer.Undo;
        Joycons.OnY -= BlockPlacer.Rotate;

        Joycons.OnLeftStick -= Cursor.Move;

        BlockPlacer.Toggle();
        base.Disable();
    }
}