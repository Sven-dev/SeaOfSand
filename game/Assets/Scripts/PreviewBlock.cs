using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBlock : MonoBehaviour
{
    private MeshRenderer Mesh;

    public Color Colour
    {
        get
        {
            Color c = Mesh.material.color;
            c.a = 0;
            return c;
        }
        set { Mesh.material.color = value; }
    }

    public Material Material
    {
        get { return Mesh.material; }
    }

    // Use this for initialization
    void Awake()
    {
        Mesh = GetComponent<MeshRenderer>();
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