using UnityEngine;
using Padrao.StateMachine;
using NaughtyAttributes;
using DG.Tweening;
using System.Collections.Generic;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthBase))]
public class Player : MonoBehaviour, IDamageable
{
    public enum PlayerStates{
        IDLE,
        WALKING,
        RUNNNING,
        JUMPING
    }

    [Foldout("Components")]
    public CharacterController charController;
    [Foldout("Components")]
    public Animator animator;

    [Foldout("Movement")]
    public float speed = 1f;
    [Foldout("Movement")]
    public float turnSpeed = 1f;
    [Foldout("Movement")]
    public float speedMultiplier = 1.5f;
    [Foldout("Movement")]
    public float jumpForce = 20f;
    [Foldout("Movement")]
    public string animWalk = "WALKING";

    public float gravity = -9.8f;
    public float fallHeight = 0.2f;

    public LayerMask groundLayers;

    public StateMachine<PlayerStates> stateMachine;
    [Header("Flash")]
    public List<FlashColor> flashColors;

    private Vector3 _directionVector = Vector3.zero;
    private bool _grounded = true;
    private float _vSpeed = 0f;

    #region LIFE
    public void TakeDamage(float damage)
    {
        foreach (var item in flashColors)   
        {
            item.Flash(Color.red);
        }
    }

    public void TakeDamage(float damage, Vector3 direction)
    {
        transform.DOMove(transform.position - direction, 0.1f);
        TakeDamage(damage);
    }
    #endregion

    void OnValidate()
    {
        charController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        CheckMove();
        CheckJump();
        Rotate();
        _vSpeed -= gravity * Time.deltaTime;
        _directionVector.y = _vSpeed;
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
    }

    private void Rotate(){
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * turnSpeed, 0);
    }

    private void CheckMove(){
        _directionVector = Input.GetAxis("Vertical") * speed * transform.forward;

        if(_directionVector.magnitude > 0){
            if(Input.GetKey(KeyCode.LeftShift) && charController.isGrounded){
                _directionVector *= speedMultiplier;
                stateMachine.SwitchState(PlayerStates.RUNNNING);
            } else {
                if(charController.isGrounded){
                    stateMachine.SwitchState(PlayerStates.WALKING);
                }
            }
        } else {
            stateMachine.SwitchState(PlayerStates.IDLE);
        }
    }

    public void Move(){
        if(_directionVector.y < 0.5f && !charController.isGrounded){
            if(!Physics.Raycast(transform.position, Vector3.down, fallHeight, groundLayers))
            {
                animator.SetBool("Falling", true);
            }
        }
        charController.Move(_directionVector * Time.deltaTime);
    }

    private void CheckJump(){
        if(charController.isGrounded){
            animator.SetBool("Falling", false);
            _vSpeed = 0f;
            if(_directionVector.magnitude == 0){
                stateMachine.SwitchState(PlayerStates.IDLE);
            }
            if(Input.GetKeyDown(KeyCode.Space)){
                stateMachine.SwitchState(PlayerStates.JUMPING);
            }
        }
    }

    public void Jump(){
        _vSpeed = jumpForce;
        animator.SetTrigger("Jump");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * fallHeight));
    }

}
