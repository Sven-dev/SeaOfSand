using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Probably temp, used for prototypes
public class BlockSelecter : MonoBehaviour
{
    public Block Prefab;
    public BlockPlacer BlockPlacer;

    public void ChangeBlock()
    {
        BlockPlacer.Prefab = Prefab;
    }
}