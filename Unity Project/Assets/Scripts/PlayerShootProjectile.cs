using UnityEngine;
using System.Collections;

public class PlayerShootProjectile : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletProjectile;
    [SerializeField] private float _bulletSpeed = 20;
    
    protected void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var instantiatedProjectile = Instantiate(_bulletProjectile, transform.position, transform.rotation);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, _bulletSpeed));
        }
    }
}