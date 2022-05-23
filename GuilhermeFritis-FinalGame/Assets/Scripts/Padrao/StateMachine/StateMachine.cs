using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public enum State
{
    NONE
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
        statesDictionary.Add(State.NONE, new StateBase());
    }    

    [Button]
    public void StartGame(){

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
