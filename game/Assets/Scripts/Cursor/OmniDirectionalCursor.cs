using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmniDirectionalCursor : MonoBehaviour
{
    /// <summary>
    /// Decides wether the cursor moves diagonally or omnidirectionally
    /// </summary>
    private bool Diagonal;

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

    private void Move(float[] stick)
    {
        float mod = 1;
        Vector2 direction = Vector2.zero;

        //If the A button is pressed
        if (Joycons.A && Diagonal)
        {
            mod = 0.5f;
            //direction is diagonal
            #region Define a diagonal
            //Upright
            if (stick[0] >= 0 && stick[1] >= 0)
            {
                direction = new Vector3(1, 0.585f);
            }
            //Upleft
            else if (stick[0] < 0 && stick[1] > 0)
            {
                direction = new Vector3(-1, 0.585f);
            }
            //Downright
            else if (stick[0] >= 0 && stick[1] <= 0)
            {
                direction = new Vector3(1, -0.585f);
            }
            //Downleft
            else if (stick[0] < 0 && stick[1] < 0)
            {
                direction = new Vector3(-1, -0.585f);
            }
            #endregion
        }
        else
        {
            //Omnidirectional movement
            direction = new Vector2(stick[0], stick[1]);
        }

        transform.Translate(direction * (Speed * mod) * Time.deltaTime, Space.Self);
        Clamp(stick);
    }

    /// <summary>
    /// Clamps the cursor so it can't move outside of the camera space
    /// </summary>
    private void Clamp(float[] stick)
    {
        float x = Mathf.Clamp(rect.anchoredPosition.x, 0, Screen.width);
        float y = Mathf.Clamp(rect.anchoredPosition.y, 0, Screen.height);

        if (rect.anchoredPosition.x > Screen.width - 5 || rect.anchoredPosition.x < 5 || rect.anchoredPosition.y > Screen.height - 5 || rect.anchoredPosition.y < 5)
        {
            CameraManager.Move(stick);
        }
            
        rect.anchoredPosition = new Vector2(x, y);
    }
}