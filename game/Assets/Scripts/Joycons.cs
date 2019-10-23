using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Joycons : MonoBehaviour
{
    public static Joycon Left;
    public static Joycon Right;

    // Start is called before the first frame update
    void Start()
    {
        List<Joycon> Joycons = JoyconManager.Instance.j;
        Left = Joycons[0];
        Right = Joycons[1];

        if (Joycons.Count < 2)
        {
            throw new Exception("JoyconMissingException: One or more joycons are missing");
        }
        else if (Joycons.Count > 2)
        {
            print("There are more than 2 Joycons connected, using the first 2");
        }
    }

    //Makes the joycon pulse while the button is pressed
    IEnumerator _Pulse()
    {
        int signum = 1;
        float time = 0;
        while(Right.GetButton(Joycon.Button.DPAD_RIGHT))
        {
            float amp = Mathf.Lerp(0.1f, 0.6f, time);
            Right.SetRumble(160, 320, amp);

            time += Time.deltaTime * 2.5f * signum;

            if (time >= 1)
            {
                signum = -1;
            }
            else if (time <= 0)
            {
                signum = 1;
            }

            yield return null;
        }
    }
}