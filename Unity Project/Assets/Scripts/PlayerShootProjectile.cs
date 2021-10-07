using UnityEngine;
using System.Collections;

public class PlayerShootProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody projectile;
    [SerializeField] private float speed = 20;
    
    protected void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var instantiatedProjectile = Instantiate(projectile, transform.position, transform.rotation);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, speed));
        }
    }
}