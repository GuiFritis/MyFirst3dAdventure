using System;
using UnityEngine;
using Padrao.StateMachine;
using NaughtyAttributes;
using System.Collections.Generic;
using Padrao.Core.Singleton;
using System.Collections;
using Clothing;
using Save;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(HealthBase))]
public class Player : Singleton<Player>
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

    [Foldout("Health")]
    public HealthBase health;

    public List<Collider> colliders;

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
    [Header("Clothing")]
    [SerializeField]
    private ClothingChanger _clothChanger;

    private Vector3 _directionVector = Vector3.zero;
    private bool _grounded = true;
    private float _vSpeed = 0f;
    private float _jumpForceMultiplier = 1f;
    private bool _paused = false;

    #region LIFE
    private void TakeDamage(HealthBase hp)
    {
        EffectsManager.Instance.ChangeVignette();
        foreach (var item in flashColors)   
        {
            item.FlashToColor(Color.red);
        }
        ShakeCamera.Instance.Shake(5, 5, 0.15f);
    }

    private void Death(HealthBase hp)
    {
        animator.SetTrigger("Death");
        foreach (var item in colliders)
        {
            item.enabled = false;
        }

        Invoke(nameof(Revive), 2f);
    }

    private void Revive()
    {
        health.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), 0.5f);
    }
    #endregion

    private void TurnOnColliders()
    {
        foreach (var item in colliders)
        {
            item.enabled = true;
        }
    }

    void OnValidate()
    {
        charController = gameObject.GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();
        health = gameObject.GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        health.OnDamage += TakeDamage;
        health.OnDeath += Death;
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if(!_paused)
        {
            CheckMove();
            CheckJump();
            Rotate();
            _vSpeed -= gravity * Time.deltaTime;
            _directionVector.y = _vSpeed;
            Move();
        }
    }

    public void Init(){
        stateMachine = new StateMachine<PlayerStates>();

        stateMachine.Init();
        stateMachine.statesDictionary.Add(PlayerStates.IDLE, new PlayerIdle());
        stateMachine.statesDictionary.Add(PlayerStates.WALKING, new PlayerWalking());
        stateMachine.statesDictionary.Add(PlayerStates.RUNNNING, new PlayerRunning());
        stateMachine.statesDictionary.Add(PlayerStates.JUMPING, new PlayerJumping());

        stateMachine.SwitchState(PlayerStates.IDLE);

        SaveManager.Instance.OnGameLoaded += OnLoad;
    }

    private void OnLoad(SaveSetup saveSetup)
    {
        if(saveSetup.health > 0)
        {
            health.ResetLife(saveSetup.health);
        } 
        else 
        {
            health.ResetLife();
        }
        _clothChanger.ChangeTexture(ClothingManager.Instance.GetSetupByType(saveSetup.clothing));
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

    public void Move()
    {
        if(_directionVector.y < 0.5f && !charController.isGrounded)// && !Physics.Raycast(transform.position, Vector3.down, fallHeight, groundLayers)){
        {    
            animator.SetBool("Falling", true);
        }
        charController.Move(_directionVector * Time.deltaTime);
    }

    private void CheckJump()
    {
        if(charController.isGrounded)// || Physics.Raycast(transform.position, Vector3.down, fallHeight, groundLayers))
        {
            animator.SetBool("Falling", false);
            _vSpeed = 0f;
            if(_directionVector.magnitude == 0)
            {
                stateMachine.SwitchState(PlayerStates.IDLE);
            }
            if(Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.SwitchState(PlayerStates.JUMPING);
            }
        }
    }

    public void Jump()
    {
        _vSpeed = jumpForce * _jumpForceMultiplier;
        animator.SetTrigger("Jump");
    }

    public void Respawn()
    {
        if(CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * fallHeight));
    }

    public void ChangeSpeed(float speedMultiplier, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speedMultiplier, duration));
    }

    private IEnumerator ChangeSpeedCoroutine(float speedMultiplier, float duration)
    {
        speed *= speedMultiplier;
        yield return new WaitForSeconds(duration);
        speed /= speedMultiplier;
    }

    public void ChangeJumpForce(float jumpForceMultiplier, float duration)
    {
        StartCoroutine(ChangeJumpForceCoroutine(jumpForceMultiplier, duration));
    }

    private IEnumerator ChangeJumpForceCoroutine(float jumpForceMultiplier, float duration)
    {
        _jumpForceMultiplier *= jumpForceMultiplier;
        yield return new WaitForSeconds(duration);
        _jumpForceMultiplier /= jumpForceMultiplier;
    }

    public void ChangeTexture(ClothingSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    private IEnumerator ChangeTextureCoroutine(ClothingSetup setup, float duration)
    {
        _clothChanger.ChangeTexture(setup);
        yield return new WaitForSeconds(duration);
        _clothChanger.ResetTexture();
    }

    public ClothingType GetClothingType()
    {
        return _clothChanger.GetClothingType();
    }

    #region PAUSE
    public void Pause()
    {
        _paused = true;
    }

    public void Unpause()
    {
        _paused = false;
    }
    #endregion

}
