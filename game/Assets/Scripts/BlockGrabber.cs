using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Allows the player to grab and let go of blocks
public class BlockGrabber : MonoBehaviour
{
    [HideInInspector]
    public bool Active = false;

    public GameObject PreviewPrefab;

    private Block GrabbedBlock;
    private Vector3 Rotation = Vector3.zero;

    /// <summary>
    /// Starts or stops the object
    /// </summary>
    public void Toggle()
    {
        Active = !Active;
    }

    /// <summary>
    /// Check if there is an object where the cursor is pointing
    /// </summary>
    public void Grab()
    {
        //Raycast on the cursor position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Check if the side of a cube is hit
            if (hit.collider.tag == "CubeEdge" && hit.transform.parent.parent.tag != "Immovable")
            {
                //Grab the block
                GrabbedBlock = hit.transform.GetComponentInParent<Block>();
                GrabbedBlock.Toggle();

                StartCoroutine(_Grab());
            }
        }

        Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow, 3);
    }

    IEnumerator _Grab()
    {
        //Instantiate a preview of the block
        Transform Preview = Instantiate(PreviewPrefab, GrabbedBlock.transform.position, Quaternion.Euler(Rotation)).transform;
        while (Active && Joycons.A)
        {
            //Raycast on the cursor position
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Check if a block is hit
                if (hit.collider.tag == "CubeEdge")
                {
                    //Move the preview
                    Preview.position = hit.transform.position;
                }
            }

            //Rotate the preview left or right
            if (Joycons.LeftBumper)
            {
                Rotation -= Vector3.up * 90;
                Preview.rotation = Quaternion.Euler(Rotation);
            }
            else if (Joycons.RightBumper)
            {
                Rotation += Vector3.up * 90;
                Preview.rotation = Quaternion.Euler(Rotation);
            }
            
            yield return null;
        }

        //Add the action to the actionlist
        ActionManager.AddAction(new PositionTracker(GrabbedBlock, GrabbedBlock.transform.position));

        //Place the grabbed block on the preview position
        GrabbedBlock.transform.position = Preview.position;
        GrabbedBlock.transform.rotation = Quaternion.Euler(Rotation);
        GrabbedBlock.Toggle();

        //Destroy the preview and reset the grabber
        Destroy(Preview.gameObject);
        GrabbedBlock = null;
        Rotation = Vector3.zero;
    }
}