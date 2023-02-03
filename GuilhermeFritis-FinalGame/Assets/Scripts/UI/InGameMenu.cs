using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class InGameMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public string floatParam = "MasterVolume";

    public void ReturnToGame()
    {
        GameManager.Instance.UnpauseGame();
    }

    public void ChangeVolume(float f){
        audioMixer.SetFloat(floatParam, f);
    }

    public void BackToMenu()
    {
        GameManager.Instance.UnpauseGame();
        LoadScene.Load(0);
    }

    public void QuitGame()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else            
        Application.Quit();
    #endif
    }
}
