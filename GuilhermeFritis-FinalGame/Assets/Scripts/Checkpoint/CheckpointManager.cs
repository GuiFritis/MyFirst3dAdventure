using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using TMPro;
using DG.Tweening;
using Save;

public class CheckpointManager : Singleton<CheckpointManager>
{
    public int lastCheckpoint = 0;
    public List<CheckpointBase> checkpoints = new List<CheckpointBase>();
    public TextMeshProUGUI checkpointSavedText;

    protected override void Awake()
    {
        base.Awake();
        SaveManager.Instance.OnGameLoaded += OnLoad;
    }

    private void OnLoad(SaveSetup saveSetup)
    {
        lastCheckpoint = saveSetup.checkpoint;
        Player.Instance.Respawn();
    }

    public void SaveCheckpoint(int key)
    {
        if(key > lastCheckpoint)
        {
            lastCheckpoint = key;
        }
        if(checkpointSavedText != null)
        {
            AnimateText();
        }
        SaveManager.Instance.SaveCheckpoint(lastCheckpoint);
    }

    private void AnimateText()
    {
        checkpointSavedText.DOColor(Color.white, 2f);
        checkpointSavedText.DOColor(Color.clear, 2f).SetDelay(2f);
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
