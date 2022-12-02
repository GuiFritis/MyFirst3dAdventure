using UnityEngine;
using DG.Tweening;
using Padrao.Core.Utils;

[RequireComponent(typeof(HealthBase))]
public class DestructableBase : MonoBehaviour
{
    public HealthBase health;
    public SOAnimation hitShake;
    public int dropCoinsAmount = 10;
    public GameObject coinPFB;
    public float relativeDropHeight = 2f;
    public MeshFilter treeMeshFilter;
    public Mesh deadTreeMesh;


    void OnValidate()
    {
        if(health == null)
        {
            health = GetComponent<HealthBase>();
        }
    }

    void Start()
    {
        health.OnDamage += OnHit;
    }

    private void OnHit(HealthBase hp)
    {
        hitShake.DGAnimate(transform.DOShakeScale(hitShake.duration, Vector3.up/2, Mathf.RoundToInt(hitShake.value)));
        DropCoins();
    }

    private void DropCoins()
    {
        if(dropCoinsAmount > 0)
        {
            if(Random.Range(0, 2) == 0)
            {
                var coin = Instantiate(coinPFB);
                coin.transform.position = transform.position.RandowPositionAround3D(4f);
                coin.transform.position += Vector3.up * relativeDropHeight;
                coin.transform.DOScale(0, 0.2f).From();
                dropCoinsAmount--;
            }
        } 
        else
        {
            KillTree();
        }
    }

    private void KillTree()
    {
        health.OnDamage -= OnHit;
        treeMeshFilter.mesh = deadTreeMesh;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0.8f, 0.2f, 0f);
        Gizmos.DrawWireCube(transform.position + Vector3.up * relativeDropHeight, Vector3.right + Vector3.forward);
    }
}
