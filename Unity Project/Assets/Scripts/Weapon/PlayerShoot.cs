using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletProjectile;
    [SerializeField] private float _bulletSpeed = 20;
    
    public static float ReloadBar;
    private bool _mayFire;
    
    protected void Start()
    {
        ReloadBar = 6;
        _mayFire = true;
    }

    protected void Update()
    {
        if (Input.GetButtonDown("Fire1") && _mayFire && !GameManager.Instance.IsPlayerDead)
        {
            var instantiatedProjectile = Instantiate(_bulletProjectile, transform.position, transform.rotation);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, _bulletSpeed));
            ReloadBar -= 1; //wait duration
            if (ReloadBar <= 0.1)
            {
                _mayFire = false;
            }
        }
        if (!_mayFire || Input.GetButtonUp("Fire2"))
        {
            Reloading();
        }
    }

    private void Reloading()
    {
        ReloadBar += Time.deltaTime / 2;
        _mayFire = false;
        if (ReloadBar >= 6)
        {
            _mayFire = true;
        }
    }
}