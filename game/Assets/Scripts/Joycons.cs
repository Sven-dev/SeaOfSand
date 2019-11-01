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
    public delegate void OnStick(string axis, float value);
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
        else
        {
            
            StartCoroutine(_KeyboardUpdate());
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
                print("Left Trigger");
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
                print("Right Trigger");
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
                print("Left Bumper");
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
                print("Right Bumper");
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

            #region D-pad & ABYX buttons
            //D-pad
            if (Left.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                print("D-pad right");
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
                print("D-pad down");
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
                print("D-pad left");
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
                print("D-pad up");
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

            //ABYX
            if (Right.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                print("A");
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
                print("B");
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
                print("Y");
                Y = true;

                if (OnY != null)
                {
                    OnY();
                }
            }
            else if (Right.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                Y = false;
            }

            if (Right.GetButtonDown(Joycon.Button.DPAD_UP))
            {
                print("X");
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
                print("Minus");
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
                print("Plus");
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
            //Checks if the sticks x-axis is getting moved enough
            if (Mathf.Abs(stick[0]) > StickActivation)
            {
                //Start moving the cursor
                OnLeftStick("X", stick[0]);
            }
            //Checks if the sticks y-axis is getting moved enough
            if (Mathf.Abs(stick[1]) > StickActivation)
            {
                //Start moving the cursor
                OnLeftStick("Y", stick[1]);
            }

            //Right stick
            stick = Right.GetStick();
            //Checks if the sticks x-axis is getting moved enough
            if (Mathf.Abs(stick[0]) > StickActivation)
            {
                //Start moving the camera
                OnRightStick("X", stick[0]);
            }
            //Checks if the sticks y-axis is getting moved enough
            if (Mathf.Abs(stick[1]) > StickActivation)
            {
                //Start moving the camera
                OnRightStick("Y", stick[0]);
            }
            #endregion

            yield return null;
        }
    }

    IEnumerator _KeyboardUpdate()
    {
        while (true)
        {
            #region Triggers & bumpers
            //Triggers
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                print("Left Trigger");
                LeftTrigger = true;

                if (OnLeftTrigger != null)
                {
                    OnLeftTrigger();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad7))
            {
                LeftTrigger = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                print("Right Trigger");
                RightTrigger = true;

                if (OnRightTrigger != null)
                {
                    OnRightTrigger();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad9))
            {
                RightTrigger = false;
            }

            //Bumpers
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                print("Left Bumper");
                LeftBumper = true;

                if (OnLeftBumper != null)
                {
                    OnLeftBumper();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                LeftBumper = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                print("Right Bumper");
                RightBumper = true;

                if (OnRightBumper != null)
                {
                    OnRightBumper();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad3))
            {
                RightBumper = false;
            }
            #endregion

            #region D-pad & ABYX buttons
            /*
            //D-pad
            if (Joycons.Left.GetButtonDown(Joycon.Button.DPAD_RIGHT))
            {
                print("D-pad right");
                if (Joycons.OnDRight != null)
                {
                    Joycons.OnDRight();
                }
            }

            if (Joycons.Left.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                print("D-pad down");
                if (Joycons.OnDDown != null)
                {
                    Joycons.OnDDown();
                }
            }

            if (Joycons.Left.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                print("D-pad left");
                if (Joycons.OnDLeft != null)
                {
                    Joycons.OnDLeft();
                }
            }

            if (Joycons.Left.GetButtonDown(Joycon.Button.DPAD_UP))
            {
                print("D-pad up");
                if (Joycons.OnDUp != null)
                {
                    Joycons.OnDUp();
                }
            }
            */

            //ABYX
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                print("A");
                A = true;

                if (OnA != null)
                {
                    OnA();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad6))
            {
                A = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                print("B");
                B = true;

                if (OnB != null)
                {
                    OnB();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad2))
            {
                B = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                print("Y");
                Y = true;

                if (OnY != null)
                {
                    OnY();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad4))
            {
                Y = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                print("X");
                X = true;

                if (OnX != null)
                {
                    OnX();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad8))
            {
                X = false;
            }
            #endregion

            #region Plus & Minus
            //Minus
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                print("Minus");
                Minus = true;

                if (OnMinus != null)
                {
                    OnMinus();
                }
            }
            else if (Input.GetKeyUp(KeyCode.KeypadPlus))
            {
                Minus = false;
            }

            //Plus
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                print("Plus");
                Plus = true;

                if (OnPlus != null)
                {
                    OnPlus();
                }
            }
            else if (Input.GetKeyUp(KeyCode.KeypadMinus))
            {
                Plus = false;
            }
            #endregion

            #region Sticks
            //Left stick
            if (Input.GetKey(KeyCode.D))
            {
                OnLeftStick("X", 1);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                OnLeftStick("X", -1);
            }

            if (Input.GetKey(KeyCode.S))
            {
                OnLeftStick("Y", -1);
            }
            else if (Input.GetKey(KeyCode.W))
            {
                OnLeftStick("Y", 1);
            }

            //Right stick
            if (Input.GetKey(KeyCode.RightArrow))
            {
                OnRightStick("X", 1);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnRightStick("X", -1);
            }

            if (Input.GetKey(KeyCode.DownArrow))
            {
                OnRightStick("Y", -1);
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                OnRightStick("Y", 1);
            }
            #endregion

            yield return null;
        }
    }
}