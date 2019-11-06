using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [HideInInspector]
    public bool Active = true;

    private static List<Action> Actions = new List<Action>();

    private void Start()
    {
        Joycons.OnB += Undo;
    }

    /// <summary>
    /// Reverses actions from the actionlist for as long as the player holds down the button
    /// </summary>
    public void Undo()
    {
        if (!Joycons.A && !Joycons.Y && !Joycons.X)
        {
            StartCoroutine(_Undo());
        }
    }

    IEnumerator _Undo()
    {
        float timer = 0f;
        int SlowTimes = 3;
        while (Active && Joycons.B)
        {
            if (timer <= 0)
            {
                //Remove the contents of the last action
                RemoveAction();

                if (SlowTimes > 0)
                {
                    SlowTimes--;
                    timer = 0.25f;
                }
                else
                {
                    timer = 0.1f;
                }
            }

            timer -= Time.deltaTime;
            yield return null;
        }
    }

    public static void AddAction(Action action)
    {
        Actions.Add(action);
    }

    /// <summary>
    /// Removes the last action
    /// </summary>
    public static void RemoveAction()
    {
        if (Actions.Count > 0)
        {
            bool completed = Actions[Actions.Count - 1].Undo();
            Actions.RemoveAt(Actions.Count - 1);

            //If the action could not be removed, remove the record, and try again
            if (!completed)
            {
                RemoveAction();
            }
        }
    }
}

public interface Action
{
    bool Undo();
}