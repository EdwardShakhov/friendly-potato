using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletProjectile;
    [SerializeField] private float _bulletSpeed = 20;

    public static float NoReload;
    public static float ReloadBar;
    private bool _mayFire;
    
    protected void Start()
    {
        _mayFire = true;
    }

    protected void Update()
    {
        if (Input.GetButtonDown("Fire1") && _mayFire && !GameManager.Instance.IsPlayerDead)
        {
            var instantiatedProjectile = Instantiate(_bulletProjectile, transform.position, transform.rotation);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, _bulletSpeed));
            ReloadBar -= 1; //wait duration
            IsPistolEmpty();
        }
        StartReloading();
    }

    private void IsPistolEmpty()
    {
        if (ReloadBar <= 0)
        {
            _mayFire = false;
        }
    }
    
    private void StartReloading()
    {
        if (ReloadBar < 6)
        {
            ReloadBar += Time.deltaTime;
            if (ReloadBar >= 6)
            {
                _mayFire = true;
            }
        }
    }
}