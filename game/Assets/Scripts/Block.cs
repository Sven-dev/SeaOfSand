using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [HideInInspector]
    public MeshRenderer Mesh;
    private Collider Collider;
    private GameObject Colliders;

    private Color _Colour;

    public Material Material
    {
        get { return Mesh.material; }
        set { Mesh.material = value; }
    }

    /// <summary>
    /// Change the colour of the block temporarily
    /// </summary>
    public Color Colour
    {
        get { return _Colour; }
        set
        {
            Material m = new Material(Material);
            m.color = value;
            Material = m;
        }
    }

	// Use this for initialization
	void Awake()
    {
        Mesh = GetComponent<MeshRenderer>();
        Collider = GetComponent<Collider>();
        Colliders = transform.GetChild(1).gameObject;

        _Colour = Material.color;
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

    /// <summary>
    /// Change the colour of the block permanently
    /// </summary>
    /// <param name="newcolour">The new colour</param>
    public void Paint(Color newcolour)
    {
        Material m = new Material(Mesh.material);
        m.color = newcolour;

        Mesh.material = m;
        _Colour = newcolour;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}