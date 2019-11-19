using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public BlockGrabber BlockGrabber;
    public Cursor Cursor;

    public override void Enable()
    {
        base.Enable();

        Joycons.OnA += BlockGrabber.Grab;
        Joycons.OnLeftStick += Cursor.Move;

        BlockGrabber.Toggle();
    }

    public override void Disable()
    {
        base.Disable();

        Joycons.OnA -= BlockGrabber.Grab;
        Joycons.OnLeftStick -= Cursor.Move;

        BlockGrabber.Toggle();
    }
}