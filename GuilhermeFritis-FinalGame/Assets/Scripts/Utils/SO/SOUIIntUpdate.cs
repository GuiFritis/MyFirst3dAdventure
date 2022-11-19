using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SOUIIntUpdate : MonoBehaviour
{
    
    public SOInt soInt;

    public TextMeshProUGUI UITextValue;

    void Start()
    {
        UITextValue.text = "X " + soInt.value.ToString();
    }

    void Update()
    {
        UITextValue.text = "X " + soInt.value.ToString();
    }

}
