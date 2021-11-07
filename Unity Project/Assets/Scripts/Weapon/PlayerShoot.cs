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
        if (GameManager.Instance.IsPlayerDead) return;
        if (Input.GetButtonDown("Fire1") && _mayFire)   //fire
        {
            var instantiatedProjectile = Instantiate(_bulletProjectile, transform.position, transform.rotation);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, _bulletSpeed));
            ReloadBar -= 1; //wait duration
            if (ReloadBar <= 0.1)
            {
                _mayFire = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.R) || !_mayFire)    //reloading
        {
            Reloading();
        }
    }

    private void Reloading()
    {
        ReloadBar += Time.deltaTime;
        _mayFire = false;
        if (ReloadBar >= 6)
        {
            _mayFire = true;
        }
    }
}