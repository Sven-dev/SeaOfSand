using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBlock : Action
{
    private Block Block;

    public DestroyedBlock(Block block)
    {
        Block = block;
    }

    public bool Undo()
    {
        Block.Toggle();
        return true;
    }
}