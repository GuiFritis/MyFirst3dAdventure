using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 1f;
    public int damage = 1;
    public string playerTag = "Player";
    public float speed = 50f;
    public LayerMask hitLayer;

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    void OnTriggerEnter(Collider collision)
    {
        if(((1<<collision.gameObject.layer) & hitLayer) != 0)
        {
            var damageable = collision.gameObject.GetComponent<IDamageable>();
            if(damageable != null)
            {
                Vector3 dir = collision.transform.position - transform.position;
                dir = -dir.normalized;
                dir.y = 0f;
                damageable.TakeDamage(damage, dir);
            }
            Destroy(gameObject);
        }
    }
}
