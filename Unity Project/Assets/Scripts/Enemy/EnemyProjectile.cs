using Player;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private int _damage;
    
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
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}