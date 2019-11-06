using UnityEngine;

/// <summary>
/// Keeps track of 2 vector3 positions of a block
/// </summary>
public class PositionTracker : Action
{
    public Block Block;

    public Vector3 From;

    public PositionTracker(Block block, Vector3 from)
    {
        Block = block;
        From = from;
    }

    public bool Undo()
    {
        if (Block != null)
        {
            Block.transform.position = From;
            return true;
        }

        return false;
    }
}