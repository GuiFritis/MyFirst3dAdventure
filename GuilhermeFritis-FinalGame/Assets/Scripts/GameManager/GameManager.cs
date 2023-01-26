using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.Core.Singleton;
using Padrao.StateMachine;
using Save;

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
    }

    private void SceneSetup()
    {
        SaveManager.Instance.Load();
    }

}
