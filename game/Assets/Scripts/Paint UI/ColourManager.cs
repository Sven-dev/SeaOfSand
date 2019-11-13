using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourManager : MonoBehaviour
{
    public BlockPlacer BlockPlacer;
    public List<Block> Blocks;

    [HideInInspector]
    public int Index = 0;

    public void ChangeColour()
    {
        Index++;
        if (Index > Blocks.Count - 1)
        {
            Index = 0;
        }

        BlockPlacer.UpdateBlock(Blocks[Index]);
    }
}