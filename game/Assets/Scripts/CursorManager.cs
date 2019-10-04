using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public float TopSpeed;

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
        float speed = TopSpeed / 2;
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
                //If the A button is pressed
                if (Joycons.Right.GetButton(Joycon.Button.DPAD_RIGHT))
                {
                    //Move diagonally
                    Vector3 direction = Vector3.zero;
                    #region Define a diagonal
                    //Upright
                    if (stick[0] > 0 && stick[1] > 0)
                    {
                        direction = new Vector3(1, 0.585f);
                    }
                    //Upleft
                    else if (stick[0] < 0 && stick[1] > 0)
                    {
                        direction = new Vector3(-1, 0.585f);
                    }
                    //Downright
                    else if (stick[0] > 0 && stick[1] < 0)
                    {
                        direction = new Vector3(1, -0.585f);
                    }
                    //Downleft
                    else if (stick[0] < 0 && stick[1] < 0)
                    {
                        direction = new Vector3(-1, -0.585f);
                    }
                    #endregion

                    transform.Translate(direction * (TopSpeed / 2) * Time.deltaTime, Space.Self);
                    Clamp();
                }
                else
                {
                    //Move omnidirectionally 
                    Vector2 direction = new Vector2(stick[0], stick[1]);
                    transform.Translate(direction * speed * Time.deltaTime, Space.Self);
                    Clamp();

                    if (speed <= TopSpeed)
                    {
                        speed += TopSpeed * Time.deltaTime;
                    }
                }
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