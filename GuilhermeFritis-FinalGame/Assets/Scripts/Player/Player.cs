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
    [Foldout("Components")]
    public Animator animator;

    [Foldout("Movement")]
    public float speed = 1f;
    [Foldout("Movement")]
    public float speedMultiplier = 1.5f;
    [Foldout("Movement")]
    public float jumpForce = 20f;
    [Foldout("Movement")]
    public string animWalk = "WALKING";

    public LayerMask groundLayers;

    public StateMachine<PlayerStates> stateMachine;

    private Vector2 vectorMove = Vector2.zero;
    private float velocity = 1f;
    private bool grounded = true;

    void OnValidate()
    {
        rigidbody = gameObject.GetComponent<Rigidbody>();
        animator = gameObject.GetComponentInChildren<Animator>();
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
        stateMachine.statesDictionary.Add(PlayerStates.IDLE, new PlayerIdle());
        stateMachine.statesDictionary.Add(PlayerStates.WALKING, new PlayerWalking());
        stateMachine.statesDictionary.Add(PlayerStates.RUNNNING, new PlayerRunning());
        stateMachine.statesDictionary.Add(PlayerStates.JUMPING, new PlayerJumping());

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
                velocity = speed * speedMultiplier;
                stateMachine.SwitchState(PlayerStates.RUNNNING);
            } else {
                velocity = speed;
                stateMachine.SwitchState(PlayerStates.WALKING);
            }
        } else {
            stateMachine.SwitchState(PlayerStates.IDLE);
        }
    }

    public void Walk(){
        rigidbody.velocity = new Vector3(vectorMove.x * velocity, rigidbody.velocity.y, vectorMove.y * velocity);
        animator.SetBool(animWalk, true);
    }

    private void Jump(){
        if(grounded){
            if(Input.GetKeyDown(KeyCode.Space)){
                stateMachine.SwitchState(PlayerStates.JUMPING);
                rigidbody.AddForce(Vector3.up * jumpForce);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(((1<<collision.gameObject.layer) & groundLayers) != 0){
            grounded = true;
        }
    }

}
