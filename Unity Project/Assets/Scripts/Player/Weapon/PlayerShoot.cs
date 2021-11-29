using System;
using Cinemachine.Utility;
using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Weapon activeWeapon;

    protected void Update()
    {
        if (GameManager.Instance.IsPlayerDead || GameManager.Instance.IsGamePaused) return;
        
        activeWeapon = GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon;
        if (Input.GetButtonDown("Fire1") && activeWeapon.MayFire && activeWeapon.BarIsNotEmpty)   //fire
        {
            GameManager.Instance.Player.GetComponent<PlayerSound>().Shoot();

            switch (activeWeapon.WeaponName)
            {
                case "Pistol":
                    RaycastShoot();
                    break;
                case "ShotGun":
                    ProjectileShoot();
                    break;
            }

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
    
    private void RaycastShoot()
    {
        RaycastHit hit;
        var ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit);
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.blue, 1f);

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<EnemyController>())
            {
                Debug.Log("Enemy Hit!");
                var hitEnemy = hit.collider.gameObject.GetComponent<EnemyController>();
                hitEnemy.EnemyHealthBar.Show();
                hitEnemy.DamageEnemy(Random.Range((int)(0.8 * activeWeapon.Damage),(int)(1.2 * activeWeapon.Damage)));
            }
            else
            {
                Debug.Log("Hit " + hit.collider.name);
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red,1f);
        }
    }

    private void ProjectileShoot()
    {
        var instantiatedProjectile = Instantiate(gameObject.GetComponent<Weapon>().BulletProjectile, _spawnPoint.position, transform.rotation);
        instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, activeWeapon.BulletSpeed));
        instantiatedProjectile.GetComponent<Bullet>().Damage = activeWeapon.Damage;
        instantiatedProjectile.GetComponent<Bullet>().BulletDistance = activeWeapon.Distance;
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