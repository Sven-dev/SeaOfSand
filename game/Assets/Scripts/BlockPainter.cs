using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPainter : MonoBehaviour
{
    [HideInInspector]
    public bool Active = false;
    [HideInInspector]
    public PaintTracker Tracker;

    public LayerMask RaycastMask;

    private Color CurrentColour;

    //The colour of the previewed block before it got changed
    private Color PreviewColour;
    private Block PreviewBlock;

    private bool Painting;

    public void Toggle()
    {
        Active = !Active;
        if (Active)
        {
            StartCoroutine(_Preview());
        }
    }

    public void SetColour(Color colour)
    {
        CurrentColour = colour;
    }

    IEnumerator _Preview()
    {
        Painting = false;       
        while (Active)
        {
            while (!Painting)
            {
                //Show the preview colour
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, RaycastMask))
                {
                    //Check if a block is hit
                    if (hit.collider.tag == "CubeEdge" && hit.transform.parent.parent.tag != "Immovable")
                    {
                        Block block = hit.transform.GetComponentInParent<Block>();
                        if (PreviewBlock == null)
                        {
                            //Set the preview block
                            PreviewBlock = block;
                            PreviewColour = block.Colour;
                        }
                        else if (block != PreviewBlock)
                        {
                            //Change the preview block
                            PreviewBlock.Colour = PreviewColour;
                            PreviewBlock = block;
                            PreviewColour = block.Colour;
                        }

                        if (PreviewBlock.Colour != CurrentColour)
                        {
                            //paint it
                            PreviewBlock.Colour = CurrentColour;
                        }
                    }
                    else if (PreviewBlock != null)
                    {
                        //Paint the block back to the original colour
                        PreviewBlock.Colour = PreviewColour;
                        PreviewBlock = null;
                    }
                }
                else if (PreviewBlock != null)
                {
                    //Paint the block back to the original colour
                    PreviewBlock.Colour = PreviewColour;
                    PreviewBlock = null;
                }

                yield return null;
            }

            if (PreviewBlock != null)
            {
                //Paint the block back to the original colour
                PreviewBlock.Colour = PreviewColour;
                PreviewBlock = null;
            }

            while (Painting)
            {
                //Don't show the preview colour
                yield return null;
            }
        }

        PreviewBlock = null;
    }

    public void Paint()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Check if a block is hit
            if (hit.collider.tag == "CubeEdge" && hit.transform.parent.parent.tag != "Immovable")
            {
                Block block = hit.transform.GetComponentInParent<Block>();

                Tracker = new PaintTracker(block.Colour);
                ActionManager.AddAction(Tracker);

                block.Paint(CurrentColour);
                Tracker.AddBlock(block);

                StartCoroutine(_Painting());
            }
        }
    }

    IEnumerator _Painting()
    {
        Painting = true;
        while (Active && Joycons.A)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Check if a block is hit
                if (hit.collider.tag == "CubeEdge" && hit.transform.parent.parent.tag != "Immovable")
                {
                    Block block = hit.transform.GetComponentInParent<Block>();

                    block.Paint(CurrentColour);
                    Tracker.AddBlock(block);
                }
            }

            yield return null;
        }

        Painting = false;
    }
}