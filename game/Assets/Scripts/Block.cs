﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector]
    public MeshRenderer Mesh;
    private Collider Collider;
    private GameObject Colliders;

    public Material Material
    {
        get { return Mesh.material; }
        set { Mesh.material = value; }
    }

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

    public void Paint(Color newcolour)
    {
        Material m = new Material(Mesh.material);
        m.color = newcolour;

        Mesh.material = m;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}