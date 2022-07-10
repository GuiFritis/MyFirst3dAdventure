using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityBase : MonoBehaviour
{
    protected Player player;

    void OnValidate()
    {
        if(player == null)
        {
            player = gameObject.GetComponent<Player>();
        }
    }

    void Start()
    {
        Init();
        OnValidate();
        AddListeners();
    }

    void OnDestroy()
    {
        RemoveListeners();
    }

    protected virtual void Init(){ }

    protected virtual void AddListeners(){ }

    protected virtual void RemoveListeners(){ }
}
