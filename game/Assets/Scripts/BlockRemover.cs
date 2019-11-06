using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRemover : MonoBehaviour
{
    [HideInInspector]
    public bool Active = false;

    /// <summary>
    /// Remove all blocks the cursor is pointing at while A is being held
    /// </summary>
    public void Remove()
    {
        //StartCoroutine(_Remove());
    }
}
