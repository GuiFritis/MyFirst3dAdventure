using UnityEngine;
using DG.Tweening;

public class ChestBase : MonoBehaviour
{
    public Animator animator;
    public string triggerOpen = "Open";

    [Header("Notification")]
    public SpriteRenderer notification;
    public SOAnimation notificationJump;
    public SOAnimation notificationSpin;
    public float notificationFadeDuration = 0.4f;

    protected float _notificationFinalYPos;

    void OnValidate()
    {
        animator = GetComponent<Animator>();
    }

    void Awake()
    {
        _notificationFinalYPos = notification.transform.position.y;
        notification.transform.position = transform.position;
    }

    private void OpenChest()
    {
        animator.SetTrigger(triggerOpen);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {   
            ShowNotification();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {   
            HideNotification();
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
}
