using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Enemy;
using Boss;

[RequireComponent(typeof(Collider))]
public class EnemyStarter : MonoBehaviour
{
    public List<IWakeableEnemy> enemies = new List<IWakeableEnemy>();
    public List<BossBase> bosses = new List<BossBase>();
    public string playerTag = "Player";
    [Header("Events")]
    public UnityEvent OnAllEnemiesKilled;

    void Awake()
    {
        enemies = gameObject.GetComponentsInChildren<IWakeableEnemy>().ToList();
        var enemiesBase = gameObject.GetComponentsInChildren<EnemyBase>().ToList();
        foreach (var item in enemiesBase)
        {
            item.OnKill += KillEnemy;
        }

        bosses = gameObject.GetComponentsInChildren<BossBase>().ToList();
    }

    public void WakeEnemies(GameObject player)
    {
        foreach (var item in enemies)
        {
            if(item != null)
            {
                item.WakeUp(player);
            }
        }

        foreach (var item in bosses)
        {
            item.WakeUp(player);
        }
    }

    private void KillEnemy(EnemyBase enemy)
    {
        enemies.Remove((IWakeableEnemy) enemy);
        if(enemies.Count == 0)
        {
            OnAllEnemiesKilled?.Invoke();
            Destroy(gameObject, 2f);
        }
    }

    public void SleepEnemies()
    {
        foreach (var item in enemies)
        {
            if(item != null)
            {
                item.Sleep();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            WakeEnemies(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag(playerTag))
        {
            SleepEnemies();
        }
    }
}
