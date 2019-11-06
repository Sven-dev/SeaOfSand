using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPlacer : MonoBehaviour
{
    [HideInInspector]
    public bool Active = false;

    public GameObject PreviewPrefab;
    public Block Prefab;

    private float Yaxis;
    private Vector3 Rotation;

    private BlockSpawnTracker Tracker;

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
        print(Active);

        if (Active)
        {
            StartCoroutine(_Preview());
        }
    }

    IEnumerator _Preview()
    {     
        Transform Preview = null;
        while (Active)
        {
            if (!Joycons.A)
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

                if (Preview != null)
                {
                    Preview.rotation = Quaternion.Euler(Rotation);
                }
            }
            else if (Preview != null)
            {
                Destroy(Preview.gameObject);
            }

            yield return null;
        }

        Destroy(Preview.gameObject);
        Rotation = Vector3.zero;
    }

    /// <summary>
    /// Places a block if the button is being held, as long as its on the same y-axis as the first placed block
    /// </summary>
    public void Place()
    {
        //Raycast
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Check if a block is hit
            if (hit.collider.tag == "CubeEdge")
            {
                //Make a new list of blocks to remove
                List<Block> blocks = new List<Block>();

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
    }

    IEnumerator _Place()
    {
        yield return new WaitForSeconds(0.25f);
        while (Active && Joycons.A)
        {
            print("Placing");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "CubeEdge" && hit.collider.transform.position.y == Yaxis)
                {
                    InstantiateBlock(hit.transform.position);
                }
            }

            yield return null;
        }

        Yaxis = -1;
    }

    private Block InstantiateBlock(Vector3 position)
    {
        Block b = Instantiate(Prefab, position, Quaternion.Euler(Rotation));
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