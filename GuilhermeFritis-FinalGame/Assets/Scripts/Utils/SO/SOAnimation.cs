using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOAnimation : ScriptableObject
{    
    public float value = 1f;
    public float duration = 0.5f;
    public float delay = 0f;
    public Ease ease = Ease.InFlash;
    public int loops = 0;
    public LoopType loopType  = LoopType.Restart;

    public Tween DGAnimate(Tween tween){
        return tween.SetEase(ease).SetLoops(loops, loopType).SetDelay(delay);
    }

}
