using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacer : MonoBehaviour {

    public Block Prefab;

    private float Yaxis;

    public Transform CursorCube;

    // Update is called once per frame
    void Update ()
    {
		if (Joycons.Right.GetButtonDown(Joycon.Button.DPAD_RIGHT))
        {
            Raycast();
        }

        if (Joycons.Right.GetButton(Joycon.Button.DPAD_RIGHT))
        {
            Raycast(Yaxis);
        }
	}

    private void Raycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "CubeEdge")
            {
                CursorCube.position = hit.collider.transform.position;
                Yaxis = hit.collider.transform.position.y;
                PlaceBlock(hit.collider.transform.position);
            }
        }
    }

    private void Raycast(float yaxis)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            if (hit.collider.tag == "CubeEdge" && hit.collider.transform.position.y == yaxis)
            {
                PlaceBlock(hit.collider.transform.position);
            }
        }
    }

    private void PlaceBlock(Vector3 position)
    {
        print("block");
        Instantiate(Prefab, position, Quaternion.identity);
    }
}
