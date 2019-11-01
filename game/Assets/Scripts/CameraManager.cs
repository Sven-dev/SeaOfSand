using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float Speed;
    private bool Moving;

	// Use this for initialization
	void Start ()
    {
        Moving = false;

        Joycons.OnRightStick += Move;
	}

    public void Move(string axis, float value)
    {
        Vector2 direction;
        if (axis == "X")
        {
            direction = Vector2.right * value;
        }
        else if (axis == "Y")
        {
            direction = Vector2.up * value;
        }
        else
        {
            throw new System.Exception("Unknown axis");
        }

        transform.Translate(direction * Speed * Time.deltaTime, Space.Self);
    }
}