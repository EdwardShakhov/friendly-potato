using Player;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float _bulletDistance;
    public float BulletDistance
    {
        get => _bulletDistance;
        set => _bulletDistance = value;
    }

    [SerializeField] private int _damage;
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }
    
    
    protected void Update()
    {
        //Destroy bullet after 10 seconds anyway
        Destroy(gameObject, 10f);
    }
    
    protected void OnCollisionEnter (Collision collision)
    {
        var collisionPlayer = collision.gameObject.GetComponent<PlayerController>();
        if (collisionPlayer)
        {
            GameManager.Instance.Player.GetComponent<PlayerSound>().HitEnemy();
            collisionPlayer.DamagePlayer(Random.Range((int)(0.8 * _damage),(int)(1.2 * _damage)));
        }
    }
}