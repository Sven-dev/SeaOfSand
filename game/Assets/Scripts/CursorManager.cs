using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public float Speed;
    public CameraZoomer CameraZoom;
    public CameraManager CameraManager;

    private RectTransform rect;

    // Use this for initialization
    void Start ()
    {
        rect = GetComponent<RectTransform>();

        Joycons.OnLeftStick += Move;
    }

    private void Move(string axis, float value)
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
        Clamp();
    }

    /// <summary>
    /// Clamps the cursor so it can't move outside of the camera space
    /// </summary>
    private void Clamp()
    {
        float x = Mathf.Clamp(rect.anchoredPosition.x, 0, Screen.width);
        float y = Mathf.Clamp(rect.anchoredPosition.y, 0, Screen.height);
        
        if (rect.anchoredPosition.x > Screen.width - 5)
        {
            CameraManager.Move("X", 1);
        }
        else if (rect.anchoredPosition.x < 5)
        {
            CameraManager.Move("X", -1);
        }

        if (rect.anchoredPosition.y > Screen.height - 5)
        {
            CameraManager.Move("Y", 1);
        }
        else if (rect.anchoredPosition.y < 5)
        {
            CameraManager.Move("Y", -1);
        }

        rect.anchoredPosition = new Vector2(x, y);
    }
}