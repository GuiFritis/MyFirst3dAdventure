using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpoint = 0;
    public List<CheckpointBase> checkpoints = new List<CheckpointBase>();

    public void SaveCheckpoint(int key)
    {
        if(key > lastCheckpoint)
        {
            lastCheckpoint = key;
        }
    }

    public bool HasCheckpoint()
    {
        return lastCheckpoint > 0;
    }

    public Vector3 GetPositionFromLastCheckpoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckpoint);
        return checkpoint.spawnPosition.position;
    }
}
