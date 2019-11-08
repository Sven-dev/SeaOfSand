using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockRemover : MonoBehaviour
{
    [HideInInspector]
    public bool Active = false;

    /// <summary>
    /// Removes a block if the player is pointing at one
    /// </summary>
    public void Remove()
    {
        //Raycast on the cursor position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Check if a block is hit
            if (hit.collider.tag == "CubeEdge" && hit.transform.parent.parent.tag != "Immovable")
            {
                //Disable the block
                Block b = hit.transform.GetComponentInParent<Block>();
                b.Toggle();

                //Add the block to undo list
                ActionManager.AddAction(new DestroyedBlock(b));
            }
        }     
    }
}