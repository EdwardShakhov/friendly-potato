using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float _bulletDistance = 30;
    protected void Start()
    {
        //Destroys bullet after 3 seconds
        //Destroy(gameObject, 3.0f);
    }

    protected void Update()
    {
        //Destroys bullet when it's far from player
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, gameObject.transform.position) > _bulletDistance)
        {
            Destroy(gameObject);
        }
}
    
    void OnCollisionEnter (Collision collision)
    {
        Debug.Log(collision.transform.tag);
        var collisionEnemy = collision.gameObject.GetComponent<EnemyController>();
        if (collisionEnemy)
        {
            collisionEnemy.DamageEnemy(500);
        }
        Destroy(gameObject);
    }
}
