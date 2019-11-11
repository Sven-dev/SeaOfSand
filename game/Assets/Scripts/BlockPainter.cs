using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPainter : MonoBehaviour
{
    [HideInInspector]
    public bool Active = true;

    public Material CurrentColour;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Paint()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            //Check if a block is hit
            if (hit.collider.tag == "CubeEdge")
            {
                Block block = hit.transform.GetComponentInParent<Block>();
                block.Paint(CurrentColour);
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
                }
            }

            yield return null;
        }
    }
}
