using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour {

    public Block Prefab;

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
                    //Place a new block
                    Block b = PlaceBlock(hit.collider.transform.position);

                    //Make sure new blocks can only get placed at the same y-axis
                    Yaxis = b.transform.position.y;

                    //Place cursor on top of the placed cube
                    //Transform grabber = b.transform.GetChild(0);              
                    //Vector3 screenPos = Camera.main.ScreenToViewportPoint(grabber.position);        
                    //transform.localPosition = screenPos;

                    StartCoroutine(_Place());
                }
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
                    PlaceBlock(hit.collider.transform.position);
                }
            }

            yield return null;
        }
    }

    private Block PlaceBlock(Vector3 position)
    {
        Block b = Instantiate(Prefab, position, Quaternion.identity);
        return b;
    }
}