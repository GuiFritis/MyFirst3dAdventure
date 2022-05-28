using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Padrao.StateMachine;
using NaughtyAttributes;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public enum PlayerStates{
        IDLE,
        WALKING,
        RUNNNING,
        JUMPING
    }

    [Foldout("Components")]
    public Rigidbody rigidbody;

    [Foldout("Movement")]
    public float speed = 1f;

    [HideInInspector]
    public StateMachine<PlayerStates> stateMachine;

    private Vector2 vectorMove = Vector2.zero;
    private float velocity = 1f;

    void OnValidate()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        Move();
    }

    public void Init(){
        stateMachine = new StateMachine<PlayerStates>();

        stateMachine.Init();
        stateMachine.statesDictionary.Add(PlayerStates.IDLE, new StateBase());
        stateMachine.statesDictionary.Add(PlayerStates.WALKING, new StateBase());
        stateMachine.statesDictionary.Add(PlayerStates.RUNNNING, new StateBase());
        stateMachine.statesDictionary.Add(PlayerStates.JUMPING, new StateBase());

        stateMachine.SwitchState(PlayerStates.IDLE);

        velocity = speed;
    }

    private void Move(){
        if(Input.GetKeyDown(KeyCode.W)){
            vectorMove.y = 1;
        } else if(Input.GetKeyDown(KeyCode.S)){
            vectorMove.y = -1;            
        } else if ((Input.GetKeyUp(KeyCode.W) && vectorMove.y == 1) 
            || (Input.GetKeyUp(KeyCode.S) && vectorMove.y == -1)){
            vectorMove.y = 0;
        }

        if(Input.GetKeyDown(KeyCode.D)){
            vectorMove.x = 1;
        } else if(Input.GetKeyDown(KeyCode.A)){
            vectorMove.x = -1;            
        } else if ((Input.GetKeyUp(KeyCode.D) && vectorMove.x == 1) 
            || (Input.GetKeyUp(KeyCode.A) && vectorMove.x == -1)){
            vectorMove.x = 0;
        }

        if(vectorMove.magnitude > 0){
            if(Input.GetKey(KeyCode.LeftShift)){
                velocity = speed * 1.6f;
                stateMachine.SwitchState(PlayerStates.RUNNNING);
            } else {
                velocity = speed;
                stateMachine.SwitchState(PlayerStates.WALKING);
            }
            rigidbody.velocity = new Vector3(vectorMove.x * velocity, rigidbody.velocity.y, vectorMove.y * velocity);
        }

    }

}
