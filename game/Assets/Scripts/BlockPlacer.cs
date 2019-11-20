using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BlockPlacer : MonoBehaviour
{
    public LayerMask RaycastMask;

    [HideInInspector]
    public bool Active = false;

    [HideInInspector]
    public PreviewBlock Preview;
    public Block Prefab;

    private float Yaxis;
    private Vector3 Rotation;

    private BlockSpawnTracker Tracker;

    public ColourManager ColourManager;

    private void Start()
    {
        Rotation = Vector3.zero;
    }

    /// <summary>
    /// Starts or stops the object
    /// </summary>
    public void Toggle()
    {
        Active = !Active;
        if (Active)
        {
            StartCoroutine(_Preview());
        }
    }

    /// <summary>
    /// Updates the block and preview
    /// </summary>
    /// <param name="block">The new block</param>
    public void UpdateBlock(Block block)
    {
        Prefab = block;
        if (Preview != null)
        {
            Vector3 position = Preview.transform.position;
            Destroy(Preview.gameObject);
            InstantiatePreview(position);
        }
    }

    /// <summary>
    /// Instantiate a preview version of the prefab block
    /// </summary>
    private void InstantiatePreview(Vector3 position)
    {
        //Instantiate the prefab
        Preview = Instantiate(Prefab, position, Quaternion.Euler(Rotation)).gameObject.AddComponent<PreviewBlock>();

        //Change the layer, so it doesn't get tagged by raycasts
        Preview.gameObject.layer = 0;

        //Make the material 50% transparent
        StandardShaderUtils.ChangeRenderMode(Preview.Material, StandardShaderUtils.BlendMode.Transparent);
        Color c = ColourManager.Colour;
        c.a = 0.75f;
        Preview.Material.color = c;

        //Destroy any components that are exclusive to Blocks
        Destroy(Preview.GetComponent<Block>());
        foreach (Transform t in Preview.transform)
        {
            Destroy(t.gameObject);
        }
    }

    /// <summary>
    /// Shows a preview of the block where the player is pointing
    /// </summary>
    IEnumerator _Preview()
    {     
        while (Active)
        {
            if (!Joycons.A)
            {
                //Check if the player is pointing at the edge of a block
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, RaycastMask))
                {
                    //Move the preview to that position
                    if (Preview != null)
                    {
                        Preview.transform.position = hit.transform.position;
                        Preview.transform.rotation = Quaternion.Euler(Rotation);

                        //Update the preview colour
                        if (Preview.Colour != ColourManager.Colour)
                        {
                            Color c = ColourManager.Colour;
                            c.a = 0.75f;

                            Preview.Paint(c);
                        }
                    }
                    else
                    {
                        //Instantiate the preview
                        InstantiatePreview(hit.transform.position);
                    }
                }
                else if (Preview != null)
                {
                    //There can't be a block there, so don't show the preview
                    Destroy(Preview.gameObject);
                }
            }
            else if (Preview != null)
            {
                //Player is placing a block, so don't show the preview
                Destroy(Preview.gameObject);
            }

            yield return null;
        }

        Rotation = Vector3.zero;
        if (Preview != null)
        {
            Destroy(Preview.gameObject);
        }
    }

    /// <summary>
    /// Places a block if the button is being held, as long as its on the same y-axis as the first placed block.
    /// </summary>
    public void Place()
    {
        //Raycast to check if the player is pointing at a block
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, RaycastMask))
        {
            //Track the blocks being placed
            Tracker = new BlockSpawnTracker();
            ActionManager.AddAction(Tracker);

            //Place a new block
            Block b = InstantiateBlock(hit.transform.position);

            //Make sure new blocks can only get placed at the same y-axis
            Yaxis = b.transform.position.y;

            //Place cursor on top of the placed cube
            Transform grabber = b.transform.GetChild(0);
            Vector3 screenPos = Camera.main.WorldToScreenPoint(grabber.position);
            screenPos.z = 0;
            RectTransform rect = GetComponent<RectTransform>();
            rect.anchoredPosition = screenPos;

            //Start multiplace
            StartCoroutine(_Place());
        }
    }

    /// <summary>
    /// Instantiates a block where the player is pointing while the a button is held, but only on the same y-axis as the first placed block.
    /// </summary>
    IEnumerator _Place()
    {
        yield return new WaitForSeconds(0.25f);
        while (Active && Joycons.A)
        {
            //Raycast to check if the player is pointing at a block
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, RaycastMask))
            {
                //Check if the y-positions would be the same
                if (hit.collider.transform.position.y == Yaxis)
                {
                    InstantiateBlock(hit.transform.position);
                }
            }

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
            yield return null;
        }

        Yaxis = -1;
    }

    private Block InstantiateBlock(Vector3 position)
    {
        Block b = Instantiate(Prefab, position, Quaternion.Euler(Rotation));
        b.Paint(ColourManager.Colour);

        Tracker.Add(b);

        //Joycons.Left.SetRumble(160, 320, 0.25f, 50);
        //Joycons.Right.SetRumble(160, 320, 0.25f, 50);
        return b;
    }

    public void Rotate()
    {
        StartCoroutine(_Rotate());    
    }

    IEnumerator _Rotate()
    {
        Rotation -= Vector3.up * 90;
        yield return new WaitForSeconds(0.66f);

        while(Active && Joycons.Y)
        {
            Rotation -= Vector3.up * 90;
            yield return new WaitForSeconds(0.2f);
        }
    }
}