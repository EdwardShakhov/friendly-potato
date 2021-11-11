using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletProjectile;
    [SerializeField] private float _bulletSpeed = 20;
    
    private float _reloadBar; public float ReloadBar => _reloadBar;
    private bool _mayFire;
    
    protected void Start()
    {
        _reloadBar = 6;
        _mayFire = true;
    }

    protected void Update()
    {
        if (GameManager.Instance.IsPlayerDead || GameManager.Instance.IsGamePaused) return;
        
        if (Input.GetButtonDown("Fire1") && _mayFire)   //fire
        {
            var instantiatedProjectile = Instantiate(_bulletProjectile, transform.position, transform.rotation);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, _bulletSpeed));
            _reloadBar -= 1; //wait duration
            if (_reloadBar <= 0.1)
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
        _reloadBar += Time.deltaTime;
        _mayFire = false;
        if (_reloadBar >= 6)
        {
            _mayFire = true;
        }
    }
}