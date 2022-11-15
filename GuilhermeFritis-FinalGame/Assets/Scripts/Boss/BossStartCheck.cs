using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartCheck : MonoBehaviour
{
    public string playerTag = "Player";
    public GameObject bossCamera;

    void Awake()
    {
        bossCamera.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerTag))
        {
            EnableCamera();
        }
    }

    private void EnableCamera()
    {
        bossCamera.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, transform.localScale.y);
    }
}
