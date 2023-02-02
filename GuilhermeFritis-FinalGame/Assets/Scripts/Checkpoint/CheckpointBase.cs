using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckpointBase : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Transform spawnPosition;
    public int key = 01;

    private bool _isCheckpointActive = false;
    private string _checkpointKey = "checkpointKey";
    private static float _checkpointCooldown = 7f;

    void Awake()
    {
        if(spawnPosition == null)
        {
            spawnPosition = transform;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!_isCheckpointActive && other.gameObject.CompareTag("Player"))
        {
            CheckCheckpoint();
        }
    }

    private void CheckCheckpoint()
    {
        TurnCheckpointOn();
        SaveCheckpoint();
    }

    private void TurnCheckpointOn()
    {
        StartCoroutine(LerpMaterialColor(Color.white, 1f));
    }

    private void TurnCheckpointOff()
    {
        StartCoroutine(LerpMaterialColor(Color.grey, 1f));
    }

    private IEnumerator LerpMaterialColor(Color color, float duration){
        float cont = 0f;
        while(cont < duration)
        {
            meshRenderer.material.SetColor("_EmissionColor", Color.Lerp(meshRenderer.material.GetColor("_EmissionColor"), color, Time.deltaTime));
            yield return new WaitForEndOfFrame();
        }
        meshRenderer.material.SetColor("_EmissionColor", color);
    }

    private void SaveCheckpoint()
    {
        Sounds.SFX_Pool.Instance.Play(Sounds.sfxType.CHECKPOINT);
        CheckpointManager.Instance.SaveCheckpoint(key);
        _isCheckpointActive = true;
        StartCoroutine(CheckpointCooldown());
    }

    private IEnumerator CheckpointCooldown()
    {
        yield return new WaitForSeconds(_checkpointCooldown);
        _isCheckpointActive = false;

    }
}
