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
    public delegate void OnStick(float[] values);
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

    #region States
    //Buttons
    public static bool A;
    public static bool B;
    public static bool Y;
    public static bool X;

    //D-pad
    public static bool DRight;
    public static bool DDown;
    public static bool DLeft;
    public static bool DUp;

    //+/-
    public static bool Plus;
    public static bool Minus;

    //Triggers
    public static bool LeftTrigger;
    public static bool RightTrigger;

    //Bumpers
    public static bool LeftBumper;
    public static bool RightBumper;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        List<Joycon> Joycons = JoyconManager.Instance.j;
        if (Joycons.Count >= 2)
        {
            Left = Joycons[0];
            Right = Joycons[1];

            StartCoroutine(_JoyconUpdate());
        }
    }

    IEnumerator _JoyconUpdate()
    {
        while (true)
        {
            #region Triggers & bumpers
            //Triggers
            if (Left.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                LeftTrigger = true;
                if (OnLeftTrigger != null)
                {
                    OnLeftTrigger();
                }
            }
            else if (Left.GetButtonUp(Joycon.Button.SHOULDER_2))
            {
                LeftTrigger = false;
            }

            if (Right.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                RightTrigger = true;
                if (OnRightTrigger != null)
                {
                    OnRightTrigger();
                }
            }
            else if (Right.GetButtonUp(Joycon.Button.SHOULDER_2))
            {
                RightTrigger = false;
            }

            //Bumpers
            if (Left.GetButtonDown(Joycon.Button.SHOULDER_1))
            {
                LeftBumper = true;
                if (OnLeftBumper != null)
                {
                    OnLeftBumper();
                }
            }
            else if (Left.GetButtonUp(Joycon.Button.SHOULDER_1))
            {
                LeftBumper = false;
            }

            if (Right.GetButtonDown(Joycon.Button.SHOULDER_1))
            {
                RightBumper = true;
                if (OnRightBumper != null)
                {
                    OnRightBumper();
                }
            }
            else if (Right.GetButtonUp(Joycon.Button.SHOULDER_1))
            {
                RightBumper = false;
            }
            #endregion

            #region D-pad
            //D-pad
            if (Left.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                DRight = true;
                if (OnDRight != null)
                {
                    OnDRight();
                }
            }
            else if (Left.GetButtonUp(Joycon.Button.DPAD_RIGHT))
            {
                DRight = false;
            }

            if (Left.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                DDown = true;
                if (OnDDown != null)
                {
                    OnDDown();
                }
            }
            else if (Left.GetButtonUp(Joycon.Button.DPAD_DOWN))
            {
                DDown = false;
            }

            if (Left.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                DLeft = true;
                if (OnDLeft != null)
                {
                    OnDLeft();
                }
            }
            else if (Left.GetButtonUp(Joycon.Button.DPAD_LEFT))
            {
                DLeft = false;
            }

            if (Left.GetButtonDown(Joycon.Button.DPAD_UP))
            {
                DUp = true;
                if (OnDUp != null)
                {
                    OnDUp();
                }
            }
            else if (Left.GetButtonUp(Joycon.Button.DPAD_UP))
            {
                DUp = false;
            }
            #endregion

            #region ABYX
            if (Right.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                A = true;
                if (OnA != null)
                {
                    OnA();
                }
            }
            else if (Right.GetButtonUp(Joycon.Button.DPAD_RIGHT))
            {
                A = false;
            }

            if (Right.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                B = true;
                if (OnB != null)
                {
                    OnB();
                }
            }
            else if (Right.GetButtonUp(Joycon.Button.DPAD_DOWN))
            {
                B = false;
            }

            if (Right.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                Y = true;
                if (OnY != null)
                {
                    OnY();
                }
            }
            else if (Right.GetButtonUp(Joycon.Button.DPAD_LEFT))
            {
                Y = false;
            }

            if (Right.GetButtonDown(Joycon.Button.DPAD_UP))
            {
                X = true;
                if (OnX != null)
                {
                    OnX();
                }
            }
            else if (Right.GetButtonUp(Joycon.Button.DPAD_UP))
            {
                X = false;
            }
            #endregion

            #region Plus & Minus
            //Minus
            if (Left.GetButtonDown(Joycon.Button.MINUS))
            {
                Minus = true;
                if (OnMinus != null)
                {
                    OnMinus();
                }
            }
            else if (Left.GetButtonUp(Joycon.Button.MINUS))
            {
                Minus = false;
            }

            //Plus
            if (Right.GetButtonDown(Joycon.Button.PLUS))
            {
                Plus = true;
                if (OnPlus != null)
                {
                    OnPlus();
                }
            }
            else if (Right.GetButtonUp(Joycon.Button.PLUS))
            {
                Plus = false;
            }
            #endregion

            #region Sticks
            //Left stick
            float[] stick = Left.GetStick();
            //Checks if the sticks axis are getting moved enough
            if (Mathf.Abs(stick[0]) > StickActivation || Mathf.Abs(stick[1]) > StickActivation)
            {
                //Start moving the cursor
                OnLeftStick(stick);
            }

            //Right stick
            stick = Right.GetStick();
            //Checks if the sticks x-axis is getting moved enough
            if (Mathf.Abs(stick[0]) > StickActivation || Mathf.Abs(stick[1]) > StickActivation)
            {
                //Start moving the camera
                OnRightStick(stick);
            }
            #endregion

            yield return null;
        }
    }
}