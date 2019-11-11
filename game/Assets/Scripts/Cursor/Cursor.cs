using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

/// <summary>
/// Handles the movement of the cursor
/// </summary>
public class Cursor : MonoBehaviour
{
    public float Speed;
    public CameraManager CameraManager;

    private RectTransform rect;

    // Use this for initialization
    void Start ()
    {
        rect = GetComponent<RectTransform>();
    }

    /// <summary>
    /// Move the cursor based on the joystick movement
    /// </summary>
    /// <param name="stick">Values of the x and y axis of the left joystick</param>
    public virtual void Move(float[] stick)
    {
        Vector2 direction = new Vector2(stick[0], stick[1]);
        transform.Translate(direction * Speed * Time.deltaTime, Space.Self);
        Clamp(stick);
    }

    /// <summary>
    /// Clamp the cursor so it can't move outside of the camera space
    /// </summary>
    protected void Clamp(float[] stick)
    {
        float x = Mathf.Clamp(rect.anchoredPosition.x, 0, Screen.width);
        float y = Mathf.Clamp(rect.anchoredPosition.y, 0, Screen.height);

        //if the cursor is at the edge of the screen, move the camera with the cursor
        if (rect.anchoredPosition.x > Screen.width - 5 || rect.anchoredPosition.x < 5 || rect.anchoredPosition.y > Screen.height - 5 || rect.anchoredPosition.y < 5)
        {
            CameraManager.Move(stick);
        }
            
        rect.anchoredPosition = new Vector2(x, y);
    }

    public GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public void Temp()
    {
        //Set up the new Pointer Event
        m_PointerEventData = new PointerEventData(m_EventSystem);
        //Set the Pointer Event Position to that of the mouse position
        m_PointerEventData.position = rect.anchoredPosition;

        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        m_Raycaster.Raycast(m_PointerEventData, results);

        //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }
    }
}