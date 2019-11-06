using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public BlockGrabber BlockGrabber;
    public Cursor Cursor;

    public override void Enable()
    {
        Joycons.OnA += BlockGrabber.Grab;
        Joycons.OnLeftStick += Cursor.Move;

        BlockGrabber.Toggle();
        base.Enable();
    }

    public override void Disable()
    {
        Joycons.OnA -= BlockGrabber.Grab;
        Joycons.OnLeftStick -= Cursor.Move;

        BlockGrabber.Toggle();
        base.Disable();
    }
}