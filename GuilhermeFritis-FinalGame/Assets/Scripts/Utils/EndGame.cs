using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject uiEndGame;
    public GameObject uiInGame;

    public AudioSource winSFX;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag(playerTag)){
            CallEndGame();
        }
    }

    private void CallEndGame(){
        PlayWinSFX();
        uiEndGame.SetActive(true);
        uiInGame.SetActive(false);
        PauseManager.Instance.Pause();
    }

    private void PlayWinSFX(){
        if(winSFX != null){
            winSFX.Play();
        }
    }
}
