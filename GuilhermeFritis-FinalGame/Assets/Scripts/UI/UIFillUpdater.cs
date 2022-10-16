using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIFillUpdater : MonoBehaviour
{
    public Image uiImage;
    public float duration = .1f;
    public Ease ease = Ease.InBack;
    
    private Tween _curTween;

    private void OnValidate()
    {
        if(uiImage == null)
        {
            uiImage = gameObject.GetComponent<Image>();
        }
    }

    public void UpdateValue(float val)
    {
        uiImage.fillAmount = val;
    }

    public void UpdateValue(float max, float cur)
    {
        if(_curTween != null)
        {
            _curTween.Kill();
        }
        uiImage.DOFillAmount(1 - (cur/max), duration).SetEase(ease);
    }
}
