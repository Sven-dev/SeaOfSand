using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    public float Speed;

	// Use this for initialization
	void Start ()
    {
        Joycons.OnY += Rotate;
	}

    private void Rotate()
    {
        transform.Rotate(Vector3.up * 90);
    }
}