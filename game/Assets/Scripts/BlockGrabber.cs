using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows the player to grab and let go of blocks
public class BlockGrabber : MonoBehaviour {

    public bool Previewing;
    public GameObject PreviewPrefab;

    private Block Grabbed;
    private Vector3 Rotation;

    // Use this for initialization
    void Start ()
    {
        Joycons.OnA += Check;

        Rotation = Vector3.zero;
    }

    /// <summary>
    /// Check if there is an object where the cursor is pointing
    /// </summary>
    private void Check()
    {
        //Raycast on the cursor position
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Check if the side of a cube is hit
            if (hit.collider.tag == "CubeEdge" && hit.transform.parent.parent.tag != "Immovable")
            {
                //Grab the block
                Block block = hit.transform.GetComponentInParent<Block>();
                Grabbed = block;
                block.Toggle();

                StartCoroutine(_Preview());
            }
        }
    }

    IEnumerator _Preview()
    {
        Transform Preview = null;
        while (Joycons.A)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Check if a block is hit
                if (hit.collider.tag == "CubeEdge")
                {
                    //Move the preview
                    if (Preview != null)
                    {
                        Preview.position = hit.transform.position;
                    }
                    //Instantiate the preview
                    else
                    {
                        Preview = Instantiate(PreviewPrefab, hit.transform.position, Quaternion.Euler(Rotation)).transform;
                    }
                }
            }

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

        //Let go
        Grabbed.transform.position = Preview.position;
        Grabbed.Toggle();
        Grabbed = null;
        Destroy(Preview.gameObject);
        Rotation = Vector3.zero;
    }
}