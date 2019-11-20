using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// Allows the player to grab and let go of blocks
public class BlockGrabber : MonoBehaviour
{
    public LayerMask RaycastMask, MoveMask;

    [HideInInspector]
    public bool Active = false;

    private PreviewBlock Preview;
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
        //Check if the side of a cube is hit
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, RaycastMask))
        {
            //Grab the block
            GrabbedBlock = hit.transform.GetComponentInParent<Block>();

            //Turn off the colliders & mesh
            GrabbedBlock.Toggle();

            StartCoroutine(_Grab());
        }

        Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow, 3);
    }

    IEnumerator _Grab()
    {
        //Instantiate a preview of the block
        InstantiatePreview(GrabbedBlock.transform.position);
        while (Active && Joycons.A)
        {
            //Raycast on the cursor position
            //Check if a block is hit
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, MoveMask))
            {
                //Move the preview
                Preview.transform.position = hit.transform.position;
            }
            Debug.DrawLine(transform.position, transform.TransformDirection(Vector3.forward) * 100, Color.yellow, 3);

            //Rotate the preview left or right
            if (Joycons.LeftBumper)
            {
                Rotation -= Vector3.up * 90;
                Preview.transform.rotation = Quaternion.Euler(Rotation);
            }
            else if (Joycons.RightBumper)
            {
                Rotation += Vector3.up * 90;
                Preview.transform.rotation = Quaternion.Euler(Rotation);
            }
            
            yield return null;
        }

        //Add the action to the actionlist
        ActionManager.AddAction(new PositionTracker(GrabbedBlock, GrabbedBlock.transform.position));

        //Place the grabbed block on the preview position
        GrabbedBlock.transform.position = Preview.transform.position;
        GrabbedBlock.transform.rotation = Quaternion.Euler(Rotation);
        GrabbedBlock.Toggle();

        //Destroy the preview and reset the grabber
        Destroy(Preview.gameObject);
        GrabbedBlock = null;
        Rotation = Vector3.zero;
    }

    /// <summary>
    /// Instantiate a preview version of the prefab block
    /// </summary>
    private void InstantiatePreview(Vector3 position)
    {
        //Instantiate the prefab
        Preview = Instantiate(GrabbedBlock, position, Quaternion.Euler(Rotation)).gameObject.AddComponent<PreviewBlock>();

        //Change the layer, so it doesn't get tagged by raycasts
        Preview.gameObject.layer = 0;

        //Make the material 50% transparent
        StandardShaderUtils.ChangeRenderMode(Preview.Material, StandardShaderUtils.BlendMode.Transparent);
        Color c = GrabbedBlock.Colour;
        c.a = 0.75f;
        Preview.Material.color = c;

        //Destroy any components that are exclusive to Blocks
        Block b = Preview.GetComponent<Block>();
        b.Toggle();
        Destroy(b);
        foreach (Transform t in Preview.transform)
        {
            Destroy(t.gameObject);
        }

        if (GrabbedBlock.name == Preview.name)
        {
            print("ash");
        }
    }
}