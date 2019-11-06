using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour {

    private MeshRenderer Mesh;
    private Collider Collider;
    private GameObject Colliders;

	// Use this for initialization
	void Start ()
    {
        Mesh = GetComponent<MeshRenderer>();
        Collider = GetComponent<Collider>();
        Colliders = transform.GetChild(1).gameObject;
	}

    /// <summary>
    /// Turns the block on or off, and toggles the collision
    /// </summary>
    public void Toggle()
    {
        print("here");
        Mesh.enabled = !Mesh.enabled;
        Collider.enabled = !Collider.enabled;
        Colliders.SetActive(Mesh.enabled);
    }
}