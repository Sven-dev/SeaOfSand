using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockPlacer : MonoBehaviour {

    public Block Prefab;
    public Image temp;

    private float Yaxis;
    private List<List<Block>> UndoList;

    private void Start()
    {
        UndoList = new List<List<Block>>();
    }

    // Update is called once per frame
    void Update ()
    {
        //If the a button is pressed
		if (Joycons.Right.GetButtonDown(Joycon.Button.DPAD_RIGHT))
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
                    Block b = PlaceBlock(hit.collider.transform.position);

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

        //Undo the last action if the B button is pressed & a isn't
        else if (Joycons.Right.GetButtonDown(Joycon.Button.DPAD_DOWN) && !Joycons.Right.GetButton(Joycon.Button.DPAD_RIGHT))
        {
            Undo();
        }
    }

    //Remove the contents of the last action (list)
    private void Undo()
    {
        StartCoroutine(_Undo());
    }

    private Block PlaceBlock(Vector3 position)
    {
        Block b = Instantiate(Prefab, position, Quaternion.identity);
        UndoList[UndoList.Count-1].Add(b);
        return b;
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

    IEnumerator _Place()
    {
        yield return new WaitForSeconds(0.25f);
        while (Joycons.Right.GetButton(Joycon.Button.DPAD_RIGHT))
        {
            if (Joycons.Right.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                print(UndoList[UndoList.Count - 1][0].name);
            }

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.tag == "CubeEdge" && hit.collider.transform.position.y == Yaxis)
                {
                    PlaceBlock(hit.collider.transform.position);
                }
            }

            yield return null;
        }
    }
}