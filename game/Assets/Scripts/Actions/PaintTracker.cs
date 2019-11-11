using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintTracker : Action
{
    public List<Block> Blocks = new List<Block>();
    public Material OldColour;

    public PaintTracker(Material old)
    {
        OldColour = new Material(old);
    }

    public void AddBlock(Block block)
    {
        Blocks.Add(block);
    }

    public bool Undo()
    {
        if (Blocks.Count > 0)
        {
            foreach (Block block in Blocks)
            {
                block.Paint(OldColour);
            }
           
            return true;
        }

        return false;
    }
}