using UnityEngine;
using DG.Tweening;
using Padrao.Core.Utils;
using Sounds;

[RequireComponent(typeof(HealthBase))]
public class DestructableBase : MonoBehaviour
{
    public HealthBase health;
    public SOAnimation hitShake;
    public int minDropCoinsAmount = 1;
    public int maxDropCoinsAmount = 10;
    public GameObject coinPFB;
    public float relativeDropHeight = 2f;
    public sfxType sfxType;
    public MeshFilter treeMeshFilter;
    public Mesh deadTreeMesh;
    
    private float _dropCoinsAmount;

    void OnValidate()
    {
        if(health == null)
        {
            health = GetComponent<HealthBase>();
        }
    }

    void Start()
    {
        _dropCoinsAmount = Random.Range(minDropCoinsAmount, maxDropCoinsAmount);
        health.OnDamage += OnHit;
    }

    private void OnHit(HealthBase hp)
    {
        hitShake.DGAnimate(transform.DOShakeScale(hitShake.duration, Vector3.up/2, Mathf.RoundToInt(hitShake.value)));
        SFX_Pool.Instance.Play(sfxType);
        DropCoins();
    }

    private void DropCoins()
    {
        if(_dropCoinsAmount > 0)
        {
            if(Random.Range(0, 2) == 0)
            {
                var coin = Instantiate(coinPFB);
                coin.transform.position = transform.position.RandowPositionAround3D(4f);
                coin.transform.position += Vector3.up * relativeDropHeight;
                coin.transform.DOScale(0, 0.2f).From();
                _dropCoinsAmount--;
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
