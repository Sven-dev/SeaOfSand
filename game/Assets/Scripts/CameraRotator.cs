using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float Speed;

	// Use this for initialization
	void Start ()
    {
        Joycons.OnLeftTrigger += RotateLeft;
        Joycons.OnRightTrigger += RotateRight;
    }

    private void RotateLeft()
    {
        if (!Joycons.A)
        {
            transform.Rotate(Vector3.up * 90);
        }
    }

    private void RotateRight()
    {
        if (!Joycons.A)
        {
            transform.Rotate(Vector3.up * -90);
        }
    }
}