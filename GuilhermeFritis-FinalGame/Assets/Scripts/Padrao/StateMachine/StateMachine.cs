using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Padrao.StateMachine{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> statesDictionary;

        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState{
            get {return _currentState;}
        }

        public void Init(){
            statesDictionary = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            statesDictionary.Add(typeEnum, state);
        }    

        [Button]
        public void StartGame(){
            
        }

        public void SwitchState(T state)
        {
            if(_currentState != null)
            {
                _currentState.OnStateExit();
            }

            _currentState = statesDictionary[state];

            _currentState.OnStateEnter();
        }

        private void Update()
        {
            if(_currentState != null)
            {
                _currentState.OnStateStay();
            }
        }

    }
}
