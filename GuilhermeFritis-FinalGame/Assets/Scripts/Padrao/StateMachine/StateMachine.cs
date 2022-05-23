using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    MENU,
    PLAYING,
    RESETING_POSITION,
    END_GAME
}

public class StateMachine : MonoBehaviour
{

    public static StateMachine Instance;

    public Dictionary<State, StateBase> statesDictionary;

    public StateBase currentState;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        statesDictionary = new Dictionary<State, StateBase>();
        statesDictionary.Add(State.MENU, new StateBase());
        statesDictionary.Add(State.PLAYING, new StatePlaying());
        statesDictionary.Add(State.RESETING_POSITION, new StateBase());
        statesDictionary.Add(State.END_GAME, new StateEndGame());
    }

    private void StartGame()
    {
        SwitchState(State.MENU);
    }

    public void ResetPosition()
    {
        SwitchState(State.RESETING_POSITION);
    }

    public void SwitchState(State state)
    {
        if(currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = statesDictionary[state];

        currentState.OnStateEnter();
    }

    private void Update()
    {
        if(currentState != null)
        {
            currentState.OnStateStay();
        }
    }

}
