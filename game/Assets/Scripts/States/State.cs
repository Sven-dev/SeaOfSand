using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State : MonoBehaviour
{
    [Tooltip("Doesn't do anything in code, just shows what state is active")]
    public bool Active;

    /// <summary>
    /// Turns the related controls on
    /// </summary>
    public virtual void Enable()
    {
        Active = true;
    }

    /// <summary>
    /// Turns the related controls off
    /// </summary>
    public virtual void Disable()
    {
        Active = false;
    }
}