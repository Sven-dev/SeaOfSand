using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public float MaxSpeed;
    public CameraZoomer CameraZoom;

    private bool Moving;
    private RectTransform rect;

    // Use this for initialization
    void Start ()
    {
        Moving = false;
        rect = GetComponent<RectTransform>();

        Joycons.OnLeftStick += Move;
    }

    private void Move()
    {
        if (!Moving)
        {
            Moving = true;
            StartCoroutine(_Move());
        }
    }

    IEnumerator _Move()
    {
        //Mod makes the cursor start of slower the more the camera is zoomed out for precise movement
        float mod = 0.1f * (10 - (CameraZoom.Zoom - 2));
        while (Moving)
        {
            //Check if the stick is still tilted
            float[] stick = Joycons.Left.GetStick();
            if (Mathf.Abs(stick[0]) < 0.2f && Mathf.Abs(stick[1]) < 0.2f)
            {
                Moving = false;
            }
            else
            {
                Vector3 direction = Vector3.zero;

                //direction is omnidirectional
                direction = new Vector2(stick[0], stick[1]);
                    
                //Up the speed a little
                if (mod < 1)
                {
                    mod += Time.deltaTime;
                }               

                //Move
                transform.Translate(direction * MaxSpeed * mod * Time.deltaTime, Space.Self);
                Clamp();
            }

            yield return null;
        }
    }

    /// <summary>
    /// Clamps the cursor so it can't move outside of the camera space
    /// </summary>
    private void Clamp()
    {
        float x = Mathf.Clamp(rect.anchoredPosition.x, 0, Screen.width);
        float y = Mathf.Clamp(rect.anchoredPosition.y, 0, Screen.height);

        rect.anchoredPosition = new Vector2(x, y);
    }
}