using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [Range(0.00f, 1.00f)]
    public float X;

    [Range(0.00f, 1.00f)]
    public float Y;

    public float Speed;
    private bool Moving;

    public Transform CursorCube;

    // Use this for initialization
    void Start ()
    {
        Moving = false;
    }

    private void Update()
    {
        float[] stick = Joycons.Left.GetStick();
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
                        direction = new Vector3(1 * X, 1 * Y);
                        print("upright");
                    }
                    //Upleft
                    else if (stick[0] < 0 && stick[1] > 0)
                    {
                        direction = new Vector3(-1 * X, 1 * Y);
                        print("upleft");
                    }
                    //Downright
                    else if (stick[0] > 0 && stick[1] < 0)
                    {
                        direction = new Vector3(1 * X, -1 * Y);
                        print("downright");
                    }
                    //Downleft
                    else if (stick[0] < 0 && stick[1] < 0)
                    {
                        direction = new Vector3(-1 * X, -1 * Y);
                        print("downleft");
                    }
                    #endregion

                    transform.Translate(direction * Speed * Time.deltaTime, Space.Self);

                    //CursorCube.Translate(direction * Speed * Time.deltaTime, Space.Self);
                    //Vector2 screenPos = Camera.main.WorldToScreenPoint(CursorCube.localPosition);
                    //screenPos.z = 0;
                    //transform.localPosition ;
                    //screenPos.x -= 490.3267f;
                    //screenPos.y -= 307.8012f;

                    //GetComponent<RectTransform>().anchoredPosition= screenPos;
                    //GetComponent<RectTransform>().anchoredPosition3D = screenPos;
                }
                else
                {
                    //Move omnidirectionally 
                    Vector2 direction = new Vector2(stick[0], stick[1]);
                    transform.Translate(direction * Speed * Time.deltaTime, Space.Self);
                }
            }

            yield return null;
        }
    }
}