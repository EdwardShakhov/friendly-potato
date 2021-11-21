using System;
using Player;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody _bulletProjectile;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Weapon activeWeapon;

    protected void Update()
    {
        if (GameManager.Instance.IsPlayerDead || GameManager.Instance.IsGamePaused) return;
        
        activeWeapon = GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon;
        if (Input.GetButtonDown("Fire1") && activeWeapon.MayFire && activeWeapon.BarIsNotEmpty)   //fire
        {
            GameManager.Instance.Player.GetComponent<PlayerSound>().Shoot();
            
            var instantiatedProjectile = Instantiate(_bulletProjectile, transform.position, transform.rotation, _spawnPoint);
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, activeWeapon.BulletSpeed));
            instantiatedProjectile.GetComponent<Bullet>().Damage = activeWeapon.Damage;
            instantiatedProjectile.GetComponent<Bullet>().BulletDistance = activeWeapon.Distance;
            
            activeWeapon.CurrentDelay = activeWeapon.ShootDelay;
            activeWeapon.CurrentBar -= 1;

            if (activeWeapon.CurrentDelay > 0.01f)
            {
                activeWeapon.MayFire = false;
            }
            if (activeWeapon.CurrentBar < 0.01f)
            {
                activeWeapon.BarIsNotEmpty = false;
            }
        }
        if (!activeWeapon.MayFire)
        {
            PrepareToShoot();
        }
        if (!activeWeapon.BarIsNotEmpty || Input.GetKeyDown(KeyCode.R))
        {
            Reloading();
        }
    }

    private void PrepareToShoot()
    {
        activeWeapon.CurrentDelay -= Time.deltaTime;
        if (activeWeapon.CurrentDelay <= 0.01f)
        {
            activeWeapon.MayFire = true;
        }
    }

    private void Reloading()
    {
        activeWeapon.CurrentBar += Time.deltaTime;
        activeWeapon.BarIsNotEmpty = false;
        if (activeWeapon.CurrentBar >= activeWeapon.BarCapacity)
        {
            activeWeapon.BarIsNotEmpty = true;
        }
    }
}