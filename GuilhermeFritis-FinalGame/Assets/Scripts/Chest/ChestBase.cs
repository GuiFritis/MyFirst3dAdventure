using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public InputActionReference inputAction;
    public Animator animator;
    public string triggerOpen = "Open";
    public float showItemsDelay = 0.4f;
    [Header("Notification")]
    public SpriteRenderer notification;
    public SOAnimation notificationJump;
    public SOAnimation notificationSpin;
    public float notificationFadeDuration = 0.4f;
    [Space]
    public ChestItemBase chestItem;

    protected float _notificationFinalYPos;
    protected bool _usable = true;

    void OnValidate()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        _notificationFinalYPos = notification.transform.position.y;
        notification.transform.position = transform.position;

        inputAction.asset.Enable();
    }

    private void OpenChest(InputAction.CallbackContext ctx)
    {
        if(notification.gameObject.activeSelf && _usable)
        {
            animator.SetTrigger(triggerOpen);
            _usable = false;
            HideNotification();
            inputAction.action.performed -= OpenChest;
            Invoke(nameof(ShowItem), showItemsDelay);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && _usable)
        {   
            ShowNotification();            
            inputAction.action.performed += OpenChest;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") && _usable)
        {   
            HideNotification();
            inputAction.action.performed -= OpenChest;
        }
    }

    private void ShowNotification()
    {
        notification.transform.DOKill();
        notification.DOKill();

        notificationSpin.DGAnimate(
            notification.transform.DORotate(Vector3.up * notificationSpin.value, notificationSpin.duration)
        );

        notificationJump.DGAnimate(
            notification.transform.DOMoveY(_notificationFinalYPos, notificationJump.duration)
        );

        notification.DOFade(1f, notificationFadeDuration);
    }

    private void HideNotification()
    {
        notification.transform.DOKill();
        notification.DOKill();

        notification.transform.DORotate(transform.rotation.eulerAngles, notificationSpin.duration * notificationSpin.loops);

        notification.transform.DOMoveY(transform.position.y, notificationJump.duration);

        notification.DOFade(0f, notificationFadeDuration);
    }

    private void ShowItem()
    {
        chestItem?.ShowItem();
        Invoke(nameof(CollectItem), showItemsDelay);
    }

    private void CollectItem()
    {
        chestItem?.Collect();
    }
}
