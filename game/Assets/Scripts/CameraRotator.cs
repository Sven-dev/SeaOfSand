using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        Joycons.OnLeftTrigger += RotateLeft;
        Joycons.OnRightTrigger += RotateRight;
	}

    private void RotateLeft()
    {
        transform.Rotate(Vector3.up  * -90);
    }

    private void RotateRight()
    {
        transform.Rotate(Vector3.up * 90);
    }
}