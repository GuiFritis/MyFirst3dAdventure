using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Save;
using TMPro;

public class PlayLevel : MonoBehaviour
{
    public TextMeshProUGUI uiTextName;

    void Start() {
        SaveManager.Instance.OnGameLoaded += OnLoad;
    }

    private void OnLoad(SaveSetup saveSetup)
    {
        uiTextName.text = "Play " + saveSetup.lastLevel;
    }

    void OnDestroy()
    {
        SaveManager.Instance.OnGameLoaded -= OnLoad;
    }
}
