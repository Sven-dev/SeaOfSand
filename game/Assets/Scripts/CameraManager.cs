using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private float Speed;
    private float MaxSpeed;

	// Use this for initialization
	void Start ()
    {
        Speed = 0;
        MaxSpeed = 1;

        Controller.OnRightStick += Move;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// moves the camera in one of 8 axis
    /// </summary>
    public void Move(Stick stick)
    {
        StartCoroutine(_Move(axis, value));
    }

    IEnumerator _Move(Stick stick, string axis, int direction)
    {
        if (axis == "X")
        {
            Controller.Stick2X;
        }
        else // if (axis == "Y")
        {
            Controller.Stick2Y;
        }

        while ()
        {
            transform.Translate(direction * );
        }
    }
}