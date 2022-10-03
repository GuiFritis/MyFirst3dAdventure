using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyStarter : MonoBehaviour
{
    public List<IWakeableEnemy> enemies = new List<IWakeableEnemy>();
    public string playerTag = "Player";

    void Awake()
    {
        enemies = gameObject.GetComponentsInChildren<IWakeableEnemy>().ToList();
    }

    public void WakeEnemies(GameObject player)
    {
        foreach (var item in enemies)
        {
            item.WakeUp(player);
        }
    }

    public void SleepEnemies()
    {
        foreach (var item in enemies)
        {
            item.Sleep();
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
