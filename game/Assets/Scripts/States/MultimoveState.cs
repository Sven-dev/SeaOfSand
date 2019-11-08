using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultimoveState : State
{
    public Cursor Cursor;

    public override void Enable()
    {
        base.Enable();

        Joycons.OnLeftStick += Cursor.Move;
    }

    public override void Disable()
    {
        base.Disable();

        Joycons.OnLeftStick -= Cursor.Move;
    }
}