using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private List<Joycon> Joycons;

    private Stick StickLeft;
    private Stick StickRight;

    public delegate void StickMove(Stick stick);
    public static StickMove OnLeftStick;
    public static StickMove OnRightStick;

    // Start is called before the first frame update
    void Start()
    {
        Joycons = JoyconManager.Instance.j;
        StickLeft = new Stick();
        StickRight = new Stick();

        if (Joycons.Count < 2)
        {
            new System.Exception("JoyconMissingException: One or more joycons are missing");
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
        if (Joycons[0].GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            print("Left Trigger");
        }

        if (Joycons[1].GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            print("Right Trigger");
        }

        //Bumpers
        if (Joycons[0].GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            print("Left Bumper");
        }

        if (Joycons[1].GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            print("Right Bumper");
        }
        #endregion

        #region D-pad & ABYX buttons
        //D-pad
        if (Joycons[0].GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            print("D-pad right");
        }

        if (Joycons[0].GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            print("D-pad down");
        }

        if (Joycons[0].GetButtonDown(Joycon.Button.DPAD_LEFT))
        {
            print("D-pad left");
        }

        if (Joycons[0].GetButtonDown(Joycon.Button.DPAD_UP))
        {
            print("D-pad up");
        }

        //ABYX
        if (Joycons[1].GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            print("A");
        }

        if (Joycons[1].GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            print("B");
        }

        if (Joycons[1].GetButtonDown(Joycon.Button.DPAD_LEFT))
        {
            print("Y");
        }

        if (Joycons[1].GetButtonDown(Joycon.Button.DPAD_UP))
        {
            print("X");
        }
        #endregion

        #region Plus & Minus
        //Minus
        if (Joycons[0].GetButtonDown(Joycon.Button.MINUS))
        {
            print("Minus");
        }

        //Plus
        if (Joycons[1].GetButtonDown(Joycon.Button.PLUS))
        {
            print("Plus");
        }
        #endregion

        #region Sticks
        //Left stick
        float[] stick = Joycons[0].GetStick();
        if (StickLeft.X != stick[0])
        {
            StickLeft.X = stick[0];
        }
        if (StickLeft.Y != stick[0])
        {
            StickLeft.Y = stick[1];
        }

        //Checks if the stick is getting moved enough
        if (Mathf.Abs(StickLeft.X) > 0.2f || Mathf.Abs(StickLeft.Y) > 0.2f)
        {
            //Start moving the cursor
        }

        //Right stick
        stick = Joycons[1].GetStick();
        if (StickRight.X != stick[0])
        {
            StickRight.X = stick[0];
        }
        if (StickRight.Y != stick[0])
        {
            StickRight.Y = stick[1];
        }

        //Checks if the stick is getting moved enough
        if (Mathf.Abs(StickRight.X) > 0.2f || Mathf.Abs(StickRight.Y) > 0.2f)
        {
            //Start moving the camera
        }
        #endregion
    }

    //Unsubscribe from all events
    private void OnDestroy()
    {
        List<Delegate> events = OnLeftStick.GetInvocationList().ToList();
        events.AddRange(OnRightStick.GetInvocationList().ToList());
        foreach (var d in events)
        {
            OnLeftStick -= (d as StickMove);
        }
    }
}