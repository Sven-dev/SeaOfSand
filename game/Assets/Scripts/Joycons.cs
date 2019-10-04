using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Joycons : MonoBehaviour
{
    public static Joycon Left;
    public static Joycon Right;

    [Range(0, 1f)]
    public float StickActivation = 0.2f;

    #region Events
    public delegate void OnStick();
    public static OnStick OnLeftStick;
    public static OnStick OnRightStick;
    #endregion

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

    // Update is called once per frame
    void Update()
    {
        #region Triggers & bumpers
        //Triggers
        if (Left.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            print("Left Trigger");
        }

        if (Right.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            print("Right Trigger");
        }

        //Bumpers
        if (Left.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            print("Left Bumper");
        }

        if (Right.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            print("Right Bumper");
        }
        #endregion

        #region D-pad & ABYX buttons
        //D-pad
        if (Left.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            print("D-pad right");
        }

        if (Left.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            print("D-pad down");
        }

        if (Left.GetButtonDown(Joycon.Button.DPAD_LEFT))
        {
            print("D-pad left");
        }

        if (Left.GetButtonDown(Joycon.Button.DPAD_UP))
        {
            print("D-pad up");
        }

        //ABYX
        if (Right.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            print("A");
        }

        if (Right.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            print("B");
        }

        if (Right.GetButtonDown(Joycon.Button.DPAD_LEFT))
        {
            print("Y");
        }

        if (Right.GetButtonDown(Joycon.Button.DPAD_UP))
        {
            print("X");
        }
        #endregion

        #region Plus & Minus
        //Minus
        if (Left.GetButtonDown(Joycon.Button.MINUS))
        {
            print("Minus");
        }

        //Plus
        if (Right.GetButtonDown(Joycon.Button.PLUS))
        {
            print("Plus");
        }
        #endregion

        #region Sticks
        //Left stick
        float[] stick = Left.GetStick();
        //Checks if the stick is getting moved enough
        if (Mathf.Abs(stick[0]) > StickActivation || Mathf.Abs(stick[1]) > StickActivation)
        {
            //Start moving the cursor
            OnLeftStick();
        }

        //Right stick
        stick = Right.GetStick();
        //Checks if the stick is getting moved enough
        if (Mathf.Abs(stick[0]) > StickActivation || Mathf.Abs(stick[1]) > StickActivation)
        {
            //Start moving the camera
            OnRightStick();
        }
        #endregion
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