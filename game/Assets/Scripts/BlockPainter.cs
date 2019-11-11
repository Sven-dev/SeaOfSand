using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPainter : MonoBehaviour
{
    [HideInInspector]
    public bool Active = true;

    public Material CurrentColour;

    public PaintTracker Tracker;

    public void Paint()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Check if a block is hit
            if (hit.collider.tag == "CubeEdge")
            {
                Block block = hit.transform.GetComponentInParent<Block>();

                Tracker = new PaintTracker(block.Mesh.material);
                ActionManager.AddAction(Tracker);

                block.Paint(CurrentColour);
                Tracker.AddBlock(block);

                StartCoroutine(_Painting());
            }
        }
    }

    IEnumerator _Painting()
    {
        while (Active && Joycons.A)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Check if a block is hit
                if (hit.collider.tag == "CubeEdge")
                {
                    Block block = hit.transform.GetComponentInParent<Block>();
                    block.Paint(CurrentColour);
                    Tracker.AddBlock(block);
                }
            }

            yield return null;
        }
    }
}
