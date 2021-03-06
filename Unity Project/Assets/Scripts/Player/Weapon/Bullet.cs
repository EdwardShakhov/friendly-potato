using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _bulletDistance;
    [SerializeField] private int _damage;
    [SerializeField] private ParticleSystem _hitSfx;
    private const float _destroySfxTime = 3f;
    
    public float BulletDistance
    {
        get => _bulletDistance;
        set => _bulletDistance = value;
    }
    public int Damage
    {
        get => _damage;
        set => _damage = value;
    }
    
    protected void Update()
    {
        //Destroy bullet when it's far from player
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, gameObject.transform.position) 
            > _bulletDistance)
        {
            Destroy(gameObject);
        }

        //Destroy bullet after 5 seconds anyway
        Destroy(gameObject, 5f);
    }
    
    protected void OnCollisionEnter (Collision collision)
    {
        Debug.Log(collision.transform.tag);
        Destroy(Instantiate(_hitSfx, transform.position, Quaternion.identity).gameObject, _destroySfxTime);
        var collisionEnemy = collision.gameObject.GetComponent<EnemyController>();
        if (collisionEnemy && !collisionEnemy.gameObject.GetComponent<EnemyController>().IsDead)
        {
            GameManager.Instance.Player.GetComponent<PlayerSound>().HitEnemy();
            collisionEnemy.EnemyHealthBar.Show();
            Destroy(Instantiate(collisionEnemy.BloodSfx, transform.position, Quaternion.identity)
                .gameObject, _destroySfxTime);
            collisionEnemy.DamageEnemy(Random.Range((int)(0.8 * _damage),(int)(1.2 * _damage)));
        }
        Destroy(gameObject);
    }
}
