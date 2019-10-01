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
	}

    private void Update()
    {
        float[] stick = Joycons.Right.GetStick();
        if (Mathf.Abs(stick[0]) > 0.2f || Mathf.Abs(stick[1]) > 0.2f)
        {
            if (!Moving)
            {
                StartCoroutine(_Move());
            }
        }
    }

    IEnumerator _Move()
    {
        Moving = true;
        while (Moving)
        {
            //Check if the stick is still tilted
            float[] stick = Joycons.Right.GetStick();
            if (Mathf.Abs(stick[0]) < 0.2f && Mathf.Abs(stick[1]) < 0.2f)
            {
                Moving = false;
            }
            else
            {
                Vector3 direction = new Vector3(stick[0], 0, stick[1]);
                transform.Translate( direction * Speed * Time.deltaTime, Space.Self);
            }

            yield return null;
        }
    }
}