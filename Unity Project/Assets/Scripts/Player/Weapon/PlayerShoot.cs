using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Weapon activeWeapon;
    [SerializeField] private ParticleSystem _hitSfx;
    [SerializeField] private ParticleSystem _shootSfx;
    private const float _destroySfxTime = 3f;
    
    protected void Update()
    {
        if (GameManager.Instance.IsPlayerDead || GameManager.Instance.IsGamePaused) 
            return;
        
        activeWeapon = GameManager.Instance.Player.GetComponent<PlayerController>().ActiveWeapon;
        if (Input.GetButtonDown("Fire1") && activeWeapon.MayFire && activeWeapon.BarIsNotEmpty)
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
            activeWeapon.CurrentBar--;

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
        Destroy(Instantiate(_shootSfx, 
            _spawnPoint.transform.position, _spawnPoint.transform.rotation, _spawnPoint).gameObject, _destroySfxTime);
        
        var hitEnemy = hit.collider.gameObject.GetComponent<EnemyController>();
        if (hit.collider != null)
        {
            if (hitEnemy && !hitEnemy.IsDead)
            {
                Debug.Log("Enemy Hit!");
                
                Destroy(Instantiate(hitEnemy.BloodSfx, 
                    hitEnemy.transform.position + new Vector3(0,1,0), 
                    Quaternion.identity).gameObject, _destroySfxTime);
                
                hitEnemy.EnemyHealthBar.Show();
                
                var playerStatsIncreaseCoeff = 
                    GameManager.Instance.Player.GetComponent<PlayerController>().PlayerStatsIncreaseCoeff;
                hitEnemy.DamageEnemy((int)(activeWeapon.Damage * playerStatsIncreaseCoeff 
                                                               * Random.Range(0.9f, 1.1f)));
            }
            else
            {
                Debug.Log("Hit " + hit.collider.name);
                Destroy(Instantiate(_hitSfx, 
                    hit.collider.transform.position, Quaternion.identity).gameObject, _destroySfxTime);
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red,1f);
        }
    }

    private void ProjectileShoot()
    {
        var instantiatedBullet = Instantiate(gameObject.GetComponent<Weapon>().BulletProjectile, 
            _spawnPoint.position, transform.rotation);
        Destroy(Instantiate(_shootSfx, 
            _spawnPoint.transform.position, _spawnPoint.transform.rotation, _spawnPoint).gameObject, _destroySfxTime);
        instantiatedBullet.velocity = transform.TransformDirection(new Vector3(0, 0, activeWeapon.BulletSpeed));
        instantiatedBullet.GetComponent<Bullet>().Damage = activeWeapon.Damage;
        instantiatedBullet.GetComponent<Bullet>().BulletDistance = activeWeapon.Distance;
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
        var playerStatsIncreaseCoeff = 
            GameManager.Instance.Player.GetComponent<PlayerController>().PlayerStatsIncreaseCoeff;
        activeWeapon.CurrentBar += Time.deltaTime / activeWeapon.ReloadDelay * playerStatsIncreaseCoeff;
        activeWeapon.BarIsNotEmpty = false;
        if (activeWeapon.CurrentBar >= activeWeapon.BarCapacity)
        {
            activeWeapon.BarIsNotEmpty = true;
        }
    }
}