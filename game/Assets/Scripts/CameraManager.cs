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

    public void Move(float[] stick)
    {
        Vector2 direction = new Vector2(stick[0], stick[1]);
        transform.Translate(direction * Speed * Time.deltaTime, Space.Self);
    }
}