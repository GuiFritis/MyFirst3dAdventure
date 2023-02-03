using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Save;
using Sounds;

public class EndGame : MonoBehaviour
{
    public string playerTag = "Player";
    // public GameObject uiEndGame;
    // public GameObject uiInGame;
    public List<GameObject> endGameObjects = new List<GameObject>();
    public AudioClip winSFX;
    public int currentLevel = 1;

    private bool _endGame = false;

    void Awake()
    {
        foreach (var item in endGameObjects)
        {
            item.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if(collision.transform.CompareTag(playerTag) && !_endGame){
            CallEndGame();
        }
    }

    private void CallEndGame(){
        foreach (var item in endGameObjects)
        {
            item.SetActive(true);
            item.transform.DOScale(0, .3f).SetEase(Ease.OutBack).From();
        }

        SaveManager.Instance.SaveLastLevel(currentLevel);

        PlayWinSFX();
        // uiEndGame.SetActive(true);
        // uiInGame.SetActive(false);
        Player.Instance.Pause();
        Invoke(nameof(BackToMenu), 3f);
    }

    private void BackToMenu()
    {
        LoadScene.Load(0);
    }

    private void PlayWinSFX(){
        if(winSFX != null){
            SFX_Pool.Instance.Play(winSFX);
        }
    }
}
