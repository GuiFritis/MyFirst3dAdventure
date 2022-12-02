using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Utils;
using DG.Tweening;
using Items;

public class ChestItemCoin : ChestItemBase
{
    public int coinAmount = 5;
    public GameObject coinPFB;
    public float scaleDuration = 0.2f;
    public Ease scaleEase;
    public float fadeAwayDuration = 0.5f;
    public float fadeAwayDelay = 0.1f;

    private List<GameObject> _items = new List<GameObject>();

    public override void ShowItem()
    {
        base.ShowItem();
        CreateItems();
    }

    private void CreateItems()
    {
        for (int i = 0; i < coinAmount; i++)
        {
            var item = Instantiate(coinPFB);
            item.transform.position = transform.position.RandowPositionAround3D();
            item.transform.DOScale(0, scaleDuration).SetEase(scaleEase).From();
            _items.Add(item);
        }
    }

    public override void Collect()
    {
        base.Collect();
        foreach (var item in _items)
        {
            item.transform.DOMoveY(1f, fadeAwayDuration).SetRelative().SetDelay(fadeAwayDelay * _items.IndexOf(item));
            item.transform.DOScale(0f, fadeAwayDuration/2).SetDelay(fadeAwayDelay * _items.IndexOf(item) + fadeAwayDuration/2);
            ItemManager.Instance.AddByType(ItemType.COIN);
        }
    }
}
