using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPlacer : MonoBehaviour
{
    public GameObject PreviewPrefab;
    public Block Prefab;

    public bool Previewing;

    private float Yaxis;
    private Vector3 Rotation;
    private List<List<Block>> UndoList;

    private void Start()
    {
        Rotation = Vector3.zero;
        UndoList = new List<List<Block>>();

        Joycons.OnA += Place;
        Joycons.OnB += Undo;
        Joycons.OnY += Rotate;

        Previewing = true;
        Preview();
    }

    /// <summary>
    /// Checks through raycast where the cub would be placed
    /// </summary>
    private void Preview()
    {
        StartCoroutine(_Preview());
    }

    IEnumerator _Preview()
    {
        Transform Preview = null;
        while(Previewing)
        {
            if (!Joycons.Right.GetButton(Joycon.Button.DPAD_RIGHT))
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

        Rotation = Vector3.zero;
    }

    /// <summary>
    /// Places a block if the button is being held, as long as its on the same y-axis as the first placed block
    /// </summary>
    private void Place()
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
                UndoList.Add(blocks);

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
        while (Joycons.Right.GetButton(Joycon.Button.DPAD_RIGHT))
        {
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

    /// <summary>
    /// Remove the contents of the last action (list)
    /// </summary>
    private void Undo()
    {
        StartCoroutine(_Undo());
    }

    IEnumerator _Undo()
    {
        float timer = 0f;
        int SlowTimes = 3;
        while(Joycons.Right.GetButton(Joycon.Button.DPAD_DOWN))
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                //Remove the contents of the last action (list)
                if (UndoList.Count > 0)
                {
                    foreach (Block b in UndoList[UndoList.Count - 1])
                    {
                        Destroy(b.gameObject);
                    }

                    UndoList.RemoveAt(UndoList.Count - 1);
                }

                if (SlowTimes > 0)
                {
                    SlowTimes--;
                    timer = 0.25f;
                }
                else
                {
                    timer = 0.1f;
                }
            }

            yield return null;
        }
    }

    private Block InstantiateBlock(Vector3 position)
    {
        Block b = Instantiate(Prefab, position, Quaternion.Euler(Rotation));
        UndoList[UndoList.Count-1].Add(b);

        Joycons.Left.SetRumble(160, 320, 0.25f, 50);
        Joycons.Right.SetRumble(160, 320, 0.25f, 50);
        return b;
    }

    private void Rotate()
    {
        StartCoroutine(_Rotate());
    }

    IEnumerator _Rotate()
    {
        Rotation -= Vector3.up * 90;
        yield return new WaitForSeconds(0.66f);

        while(Joycons.Right.GetButton(Joycon.Button.DPAD_LEFT))
        {
            Rotation -= Vector3.up * 90;
            yield return new WaitForSeconds(0.2f);
        }
    }
}