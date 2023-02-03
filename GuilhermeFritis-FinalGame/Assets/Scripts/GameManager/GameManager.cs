using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using Padrao.StateMachine;
using Save;
using Screens;

public class GameManager : Singleton<GameManager>
{
    public enum GameState{
        INTRO,
        GAMEPLAY,
        PAUSE, 
        WIN,
        LOSE
    }

    public StateMachine<GameState> stateMachine;
    public Player player;

    private Inputs _inputs;

    void Start()
    {
        Init();
    }

    public void Init(){        
        if(player == null){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }

        stateMachine = new StateMachine<GameState>();
        
        stateMachine.Init();
        stateMachine.RegisterStates(GameState.INTRO, new StateBase());
        stateMachine.RegisterStates(GameState.GAMEPLAY, new StateBase());
        stateMachine.RegisterStates(GameState.PAUSE, new StateBase());
        stateMachine.RegisterStates(GameState.WIN, new StateBase());
        stateMachine.RegisterStates(GameState.LOSE, new StateBase());

        stateMachine.SwitchState(GameState.INTRO);

        SceneSetup();
        if(_inputs == null)
        {
            SetInputs();
        }
    }

    private void SceneSetup()
    {
        SaveManager.Instance.Load();
    }

    private void SetInputs()
    {   
        _inputs = new Inputs();
        _inputs.Enable();

        _inputs.Gameplay.Esc.started += ctx => CallMenu();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        player.Pause();
    }

    private void CallMenu()
    {
        if(Time.timeScale == 0)
        {
            UnpauseGame();
        }
        else
        {
            //pause
            PauseGame();
            ScreenController.Instance.ShowScreen(GameplayScreenType.MENU, true);
        }
    }

    public void UnpauseGame()
    {
        //unpause
        Time.timeScale = 1;
        player.Unpause();
        ScreenController.Instance.ShowScreen(GameplayScreenType.MENU, false);
    }

}
