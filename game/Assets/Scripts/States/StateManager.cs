using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Ensures the player is able to perform certain actions based on what mode is activated
/// </summary>
public class StateManager : MonoBehaviour
{
    public int ActiveIndex = -1;
    public List<State> States;

    public Transform StateUI;

    private void Start()
    {
        Joycons.OnDRight += ChangeToMove;
        Joycons.OnDDown += ChangeToDestroy;
        Joycons.OnDLeft += ChangeToMultimove;
        Joycons.OnDUp += ChangeToBuild;

        ChangeToMove();
    }

    public void ChangeToMove()
    {
        if (ActiveIndex != 0)
        {
            Change(0);
        }
    }

    public void ChangeToDestroy()
    {
        if (ActiveIndex != 1)
        {
            Change(1);
        }
    }
    public void ChangeToMultimove()
    {
        if (ActiveIndex != 2)
        {
            Change(2);
        }
    }

    public void ChangeToBuild()
    {
        if (ActiveIndex != 3)
        {
            Change(3);
        }
    }

    private void Change(int state)
    {
        States[ActiveIndex].Disable();
        StateUI.GetChild(ActiveIndex).gameObject.SetActive(false);
        ActiveIndex = state;
        States[ActiveIndex].Enable();
        StateUI.GetChild(ActiveIndex).gameObject.SetActive(true);
    }
}