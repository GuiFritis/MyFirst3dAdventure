using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public SkinnedMeshRenderer skinnedMeshRenderer;
    [Header("Setup")]
    public float duration = .1f;
    public string colorParameter = "_EmissionColor";
    public Color targetColor = Color.clear;

    private Color startColor;
    private Tween _curTween;

    void OnValidate()
    {
        if(meshRenderer == null)
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }
        if(skinnedMeshRenderer == null)
        {
            skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        }
    }

    public void FlashToColor(Color color)
    {
        if(meshRenderer != null && !_curTween.IsActive())
        {
            _curTween = meshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
        }

        if(skinnedMeshRenderer != null && !_curTween.IsActive())
        {
            _curTween = skinnedMeshRenderer.material.DOColor(color, colorParameter, duration).SetLoops(2, LoopType.Yoyo);
        }
    }

    public void Flash()
    {
        FlashToColor(targetColor);
    }
}
