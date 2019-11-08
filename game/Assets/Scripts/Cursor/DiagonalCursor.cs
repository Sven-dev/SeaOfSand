using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves omnidirectionally normally, but only diagonally when the a button is pressed
/// </summary>
public class DiagonalCursor : Cursor
{
    public override void Move(float[] stick)
    {
        if (Joycons.A)
        {
            Vector2 direction = Vector2.zero;

            #region Define a diagonal
            //Upright
            if (stick[0] >= 0 && stick[1] >= 0)
            {
                direction = new Vector3(1, 0.585f);
            }
            //Upleft
            else if (stick[0] < 0 && stick[1] > 0)
            {
                direction = new Vector3(-1, 0.585f);
            }
            //Downright
            else if (stick[0] >= 0 && stick[1] <= 0)
            {
                direction = new Vector3(1, -0.585f);
            }
            //Downleft
            else if (stick[0] < 0 && stick[1] < 0)
            {
                direction = new Vector3(-1, -0.585f);
            }
            #endregion

            transform.Translate(direction * Speed * Time.deltaTime, Space.Self);
            Clamp(stick);
        }
        else
        {
            base.Move(stick);
        }
    }
}