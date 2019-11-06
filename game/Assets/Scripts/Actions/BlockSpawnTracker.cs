using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnTracker : Action
{
    private List<Block> Blocks = new List<Block>();

    public void Add(Block block)
    {
        Blocks.Add(block);
    }

    public bool Undo()
    {
        while (Blocks.Count > 0)
        {
            Block block = Blocks[Blocks.Count - 1];
            if (block != null)
            {
                block.Destroy();
                Blocks.Remove(block);
            }

            Blocks.Remove(block);
        }

        return true;
    }
}