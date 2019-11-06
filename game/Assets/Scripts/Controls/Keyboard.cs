using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keyboard : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(_KeyboardUpdate());
	}

    IEnumerator _KeyboardUpdate()
    {
        while (true)
        {
            #region Triggers & bumpers
            //Triggers
            if (Input.GetKeyDown(KeyCode.Keypad7))
            {
                Joycons.LeftTrigger = true;
                if (Joycons.OnLeftTrigger != null)
                {
                    Joycons.OnLeftTrigger();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad7))
            {
                Joycons.LeftTrigger = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                Joycons.RightTrigger = true;
                if (Joycons.OnRightTrigger != null)
                {
                    Joycons.OnRightTrigger();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad9))
            {
                Joycons.RightTrigger = false;
            }

            //Bumpers
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Joycons.LeftBumper = true;
                if (Joycons.OnLeftBumper != null)
                {
                    Joycons.OnLeftBumper();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad1))
            {
                Joycons.LeftBumper = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad3))
            {
                Joycons.RightBumper = true;
                if (Joycons.OnRightBumper != null)
                {
                    Joycons.OnRightBumper();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad3))
            {
                Joycons.RightBumper = false;
            }
            #endregion

            #region D-pad
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Joycons.DRight = true;
                if (Joycons.OnDRight != null)
                {
                    Joycons.OnDRight();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha1))
            {
                Joycons.DRight = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Joycons.DDown = true;
                if (Joycons.OnDDown != null)
                {
                    Joycons.OnDDown();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha2))
            {
                Joycons.DDown = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Joycons.DLeft = true;
                if (Joycons.OnDLeft != null)
                {
                    Joycons.OnDLeft();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                Joycons.DLeft = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Joycons.DUp = true;
                if (Joycons.OnDUp != null)
                {
                    Joycons.OnDUp();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Alpha4))
            {
                Joycons.DUp = false;
            }
            #endregion

            #region ABYX
            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                Joycons.A = true;
                if (Joycons.OnA != null)
                {
                    Joycons.OnA();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad6))
            {
                Joycons.A = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                Joycons.B = true;
                if (Joycons.OnB != null)
                {
                    Joycons.OnB();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad2))
            {
                Joycons.B = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                Joycons.Y = true;
                if (Joycons.OnY != null)
                {
                    Joycons.OnY();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad4))
            {
                Joycons.Y = false;
            }

            if (Input.GetKeyDown(KeyCode.Keypad8))
            {
                Joycons.X = true;
                if (Joycons.OnX != null)
                {
                    Joycons.OnX();
                }
            }
            else if (Input.GetKeyUp(KeyCode.Keypad8))
            {
                Joycons.X = false;
            }
            #endregion

            #region Plus & Minus
            //Minus
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                Joycons.Minus = true;
                if (Joycons.OnMinus != null)
                {
                    Joycons.OnMinus();
                }
            }
            else if (Input.GetKeyUp(KeyCode.KeypadPlus))
            {
                Joycons.Minus = false;
            }

            //Plus
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                Joycons.Plus = true;
                if (Joycons.OnPlus != null)
                {
                    Joycons.OnPlus();
                }
            }
            else if (Input.GetKeyUp(KeyCode.KeypadMinus))
            {
                Joycons.Plus = false;
            }
            #endregion

            #region Sticks
            //Left stick
            float[] stick = new float[] { 0, 0 };
            if (Input.GetKey(KeyCode.D))
            {
                stick[0] += 1;
            }
            if (Input.GetKey(KeyCode.A))
            {
                stick[0] -= 1;
            }
            if (Input.GetKey(KeyCode.W))
            {
                stick[1] += 1;
            }
            if (Input.GetKey(KeyCode.S))
            {
                stick[1] -= 1;
            }

            //Trigger when any key is pressed
            if (stick[0] != 0 || stick[1] != 0)
            {
                Joycons.OnLeftStick(stick);
            }

            //Right stick
            stick = new float[] { 0, 0 };
            if (Input.GetKey(KeyCode.RightArrow))
            {
                stick[0] += 1;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                stick[0] -= 1;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                stick[1] += 1;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                stick[1] -= 1;
            }

            //Trigger when any key is pressed
            if (stick[0] != 0 || stick[1] != 0)
            {
                Joycons.OnRightStick(stick);
            }
            #endregion

            yield return null;
        }
    }
}