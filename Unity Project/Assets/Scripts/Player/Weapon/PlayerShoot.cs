using System;
using Player;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletProjectile;
    [SerializeField] private float _bulletSpeed = 20;
    public static float ReloadBar;
    private float _shootDelay;
    private bool _mayFire;
    private bool _barIsNotEmpty;
    
    protected void Start()
    {
        ReloadBar = 6;
        _mayFire = true;
        _barIsNotEmpty = true;
    }

    protected void Update()
    {
        if (GameManager.Instance.IsPlayerDead || GameManager.Instance.IsGamePaused) return;
        
        if (Input.GetButtonDown("Fire1") && _mayFire && _barIsNotEmpty)   //fire
        {
            GameManager.Instance.Player.GetComponent<PlayerSound>().Shoot();
            
            var instantiatedProjectile = Instantiate(_bulletProjectile, transform.position, transform.rotation);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, _bulletSpeed));
            
            _shootDelay = 1f;
            ReloadBar -= 1;

            if (_shootDelay > 0.01f)
            {
                _mayFire = false;
            }
            if (ReloadBar < 0.01f)
            {
                _barIsNotEmpty = false;
            }
        }
        if (!_mayFire)
        {
            PrepareToShoot();
        }
        if (!_barIsNotEmpty || Input.GetKeyDown(KeyCode.R))
        {
            Reloading();
        }
    }

    private void PrepareToShoot()
    {
        _shootDelay -= Time.deltaTime;
        if (_shootDelay <= 0.01f)
        {
            _mayFire = true;
        }
    }

    private void Reloading()
    {
        ReloadBar += Time.deltaTime;
        _barIsNotEmpty = false;
        if (ReloadBar >= 6)
        {
            _barIsNotEmpty = true;
        }
    }
}