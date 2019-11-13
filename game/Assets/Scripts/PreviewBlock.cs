using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBlock : MonoBehaviour
{
    [HideInInspector]
    public MeshRenderer Mesh;

    public Material Material
    {
        get { return Mesh.material; }
        set { Mesh.material = value; }
    }

    // Use this for initialization
    void Start()
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