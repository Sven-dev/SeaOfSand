using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Joycons.Left.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            Rotate(1);
            print("Rotating Left");
        }
        else if (Joycons.Right.GetButtonDown(Joycon.Button.SHOULDER_1))
        {
            Rotate(-1);
            print("Rotating Right");
        }
	}

    private void Rotate(int signum)
    {
        transform.Rotate(Vector3.up  * 90 * signum);
    }
}
