using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector]
    public MeshRenderer Mesh;
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
        Mesh.enabled = !Mesh.enabled;
        Collider.enabled = !Collider.enabled;
        Colliders.SetActive(Mesh.enabled);
    }

    public void Paint(Material newcolour)
    {
        Mesh.material = new Material(newcolour);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}