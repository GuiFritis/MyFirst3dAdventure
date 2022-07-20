using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    [Header("Setup")]
    public float duration = .1f;

    private Color startColor;
    private Tween _curTween;

    void Start()
    {
        startColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    public void Flash(Color color)
    {
        if(!_curTween.IsActive())
        {
            meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
        }
    }
}
