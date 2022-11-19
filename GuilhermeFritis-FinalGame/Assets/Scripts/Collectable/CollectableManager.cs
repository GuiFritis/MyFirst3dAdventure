using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using TMPro;

public class CollectableManager : Singleton<CollectableManager>
{
    public SOInt coins;
    public SOInt energy;

    void Start()
    {
        Reset();
    }

    private void Reset()
    {
        coins.value = 0;
    }

    public void AddCoin(int amount = 1){
        coins.value += amount;
    }

    public void AddEnergy(int amount = 1){
        energy.value += amount;
    }
}
