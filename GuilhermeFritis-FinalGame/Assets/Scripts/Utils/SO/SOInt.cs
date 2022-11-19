using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class SOInt : ScriptableObject
{
    public Action<int> OnValueChanged;

    public int Value
    {
        get{return _value;}
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    private int _value;
}
