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
            States[ActiveIndex].Disable();
            ActiveIndex = 0;
            States[ActiveIndex].Enable();
        }
    }

    public void ChangeToDestroy()
    {
        if (ActiveIndex != 1)
        {
            States[ActiveIndex].Disable();
            ActiveIndex = 1;
            States[ActiveIndex].Enable();
        }
    }
    public void ChangeToMultimove()
    {
        if (ActiveIndex != 2)
        {
            States[ActiveIndex].Disable();
            ActiveIndex = 2;
            States[ActiveIndex].Enable();
        }
    }

    public void ChangeToBuild()
    {
        if (ActiveIndex != 3)
        {
            States[ActiveIndex].Disable();
            ActiveIndex = 3;
            States[ActiveIndex].Enable();
        }
    }
}