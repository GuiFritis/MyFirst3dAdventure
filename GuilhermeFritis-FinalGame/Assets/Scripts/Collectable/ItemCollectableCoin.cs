using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemCollectableCoin : ItemCollectableBase
{
    public SOAnimation collectingMoveY;
    public SOAnimation collectingFade;

    public SOAnimation coinFloat;

    private void Start() 
    {
        coinFloat.DGAnimate(transform.DOMoveY(coinFloat.value, coinFloat.duration));
    }

    protected override void Collect()
    {
        if(audioSorce != null){
            audioSorce.Play();
        }
        collectingMoveY.DGAnimate(transform.DOMoveY(collectingMoveY.value , collectingMoveY.duration));
        collectingFade.DGAnimate(collectableSprite.DOFade(collectingFade.value, collectingFade.duration));
        Invoke(nameof(OnCollect), collectingFade.delay);
        Invoke(nameof(HideObject), collectingFade.delay + hideDelay + collectParticleSystem.main.duration);
    }
    protected override void OnCollect()
    {
        base.OnCollect();
        CollectableManager.Instance.AddCoin();
    }
}
