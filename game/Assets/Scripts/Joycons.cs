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
    //Sticks
    public delegate void OnStick();
    public static OnStick OnLeftStick;
    public static OnStick OnRightStick;

    //Buttons
    public delegate void OnButton();
    public static OnButton OnA;
    public static OnButton OnB;
    public static OnButton OnY;
    public static OnButton OnX;

    //D-pad
    public static OnButton OnDRight;
    public static OnButton OnDDown;
    public static OnButton OnDLeft;
    public static OnButton OnDUp;

    //+/-
    public static OnButton OnPlus;
    public static OnButton OnMinus;

    //Triggers
    public static OnButton OnLeftTrigger;
    public static OnButton OnRightTrigger;

    //Bumpers
    public static OnButton OnLeftBumper;
    public static OnButton OnRightBumper;

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
            if (OnLeftTrigger != null)
            {
                OnLeftTrigger();
            }
        }

        if (Right.GetButtonDown(Joycon.Button.SHOULDER_2))
        {
            print("Right Trigger");
            if (OnRightTrigger != null)
            {
                OnRightTrigger();
            }
        }

        //Bumpers
        if (Left.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            print("Left Bumper");
            if (OnLeftBumper != null)
            {
                OnLeftBumper();
            }
        }

        if (Right.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            print("Right Bumper");
            if (OnRightBumper != null)
            {
                OnRightBumper();
            }
        }
        #endregion

        #region D-pad & ABYX buttons
        //D-pad
        if (Left.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            print("D-pad right");
            if (OnDRight != null)
            {
                OnDRight();
            }
        }

        if (Left.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            print("D-pad down");
            if (OnDDown != null)
            {
                OnDDown();
            }
        }

        if (Left.GetButtonDown(Joycon.Button.DPAD_LEFT))
        {
            print("D-pad left");
            if (OnDLeft != null)
            {
                OnDLeft();
            }
        }

        if (Left.GetButtonDown(Joycon.Button.DPAD_UP))
        {
            print("D-pad up");
            if (OnDUp != null)
            {
                OnDUp();
            }
        }

        //ABYX
        if (Right.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            print("A");
            if (OnA != null)
            {
                OnA();
            }
        }

        if (Right.GetButtonDown(Joycon.Button.DPAD_DOWN))
        {
            print("B");
            if (OnB != null)
            {
                OnB();
            }
        }

        if (Right.GetButtonDown(Joycon.Button.DPAD_LEFT))
        {
            print("Y");
            if (OnY != null)
            {
                OnY();
            }
        }

        if (Right.GetButtonDown(Joycon.Button.DPAD_UP))
        {
            print("X");
            if (OnX != null)
            {
                OnX();
            }
        }
        #endregion

        #region Plus & Minus
        //Minus
        if (Left.GetButtonDown(Joycon.Button.MINUS))
        {
            print("Minus");
            if (OnMinus != null)
            {
                OnMinus();
            }
        }

        //Plus
        if (Right.GetButtonDown(Joycon.Button.PLUS))
        {
            print("Plus");
            if (OnPlus != null)
            {
                OnPlus();
            }
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
}