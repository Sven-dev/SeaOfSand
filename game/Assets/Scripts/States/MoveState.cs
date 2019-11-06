using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : State
{
    public BlockGrabber BlockGrabber;

    public override void Enable()
    {
        Joycons.OnA += BlockGrabber.Check;

        BlockGrabber.Toggle();
        base.Enable();
    }

    public override void Disable()
    {
        Joycons.OnA -= BlockGrabber.Check;

        BlockGrabber.Toggle();
        base.Disable();
    }
}