using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyState : State
{
    public BlockRemover BlockRemover;
    public Cursor Cursor;

    public override void Enable()
    {
        base.Enable();

        Joycons.OnA += BlockRemover.Remove;
        Joycons.OnLeftStick += Cursor.Move;
    }

    public override void Disable()
    {
        base.Disable();

        Joycons.OnA -= BlockRemover.Remove;
        Joycons.OnLeftStick -= Cursor.Move;
    }
}