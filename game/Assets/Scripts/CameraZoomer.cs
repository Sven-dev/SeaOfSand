using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomer : MonoBehaviour
{
    [HideInInspector]
    public int Zoom;
    private Camera Camera;

	// Use this for initialization
	void Start ()
    {
        Camera = GetComponent<Camera>();
        Zoom = (int)Camera.orthographicSize;

        Joycons.OnLeftBumper += ZoomIn;
        Joycons.OnRightBumper += ZoomOut;
    }

    private void ZoomIn()
    {
        if (Camera.orthographicSize < 8)
        {
            Camera.orthographicSize++;
            Zoom++;
        }      
    }

    private void ZoomOut()
    {
        if (Camera.orthographicSize > 3)
        {
            Camera.orthographicSize--;
            Zoom--;
        }
    }
}