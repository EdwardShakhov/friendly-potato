using Player;
using UnityEngine;

public class Bullet : MonoBehaviour
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
        //Destroys bullet when it's far from player
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, gameObject.transform.position) > _bulletDistance)
        {
            Destroy(gameObject);
        }
    }
    
    protected void OnCollisionEnter (Collision collision)
    {
        Debug.Log(collision.transform.tag);
        var collisionEnemy = collision.gameObject.GetComponent<EnemyController>();
        if (collisionEnemy)
        {
            GameManager.Instance.Player.GetComponent<PlayerSound>().HitEnemy();
            collisionEnemy.EnemyHealthBar.Show();
            collisionEnemy.DamageEnemy(Random.Range((int)(0.8 * _damage),(int)(1.2 * _damage)));
        }
        Destroy(gameObject);
    }
}
