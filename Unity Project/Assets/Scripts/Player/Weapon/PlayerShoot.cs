using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Weapon activeWeapon;
    [SerializeField] private ParticleSystem _hitSfx;
    [SerializeField] private ParticleSystem _shootSfx;

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
        Destroy(Instantiate(_shootSfx, _spawnPoint.transform.position, _spawnPoint.transform.rotation, _spawnPoint).gameObject, 3f);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.GetComponent<EnemyController>() && !hit.collider.gameObject.GetComponent<EnemyController>().IsDead)
            {
                Debug.Log("Enemy Hit!");
                Destroy(Instantiate(hit.collider.gameObject.GetComponent<EnemyController>().BloodSfx, 
                    hit.collider.gameObject.transform.position, Quaternion.identity).gameObject, 3f);
                var hitEnemy = hit.collider.gameObject.GetComponent<EnemyController>();
                var PlayerStatsIncreaseCoeff = GameManager.Instance.Player.GetComponent<PlayerController>().PlayerStatsIncreaseCoeff;
                hitEnemy.EnemyHealthBar.Show();
                hitEnemy.DamageEnemy((int) (Random.Range((int)(0.8 * activeWeapon.Damage),(int)(1.2 * activeWeapon.Damage))
                                            * PlayerStatsIncreaseCoeff));
            }
            else
            {
                Debug.Log("Hit " + hit.collider.name);
                Destroy(Instantiate(_hitSfx, hit.collider.transform.position, Quaternion.identity).gameObject, 3f);
            }
            Debug.DrawLine(ray.origin, hit.point, Color.red,1f);
        }
    }

    private void ProjectileShoot()
    {
        var instantiatedProjectile = Instantiate(gameObject.GetComponent<Weapon>().BulletProjectile, _spawnPoint.position, transform.rotation);
        Destroy(Instantiate(_shootSfx, _spawnPoint.transform.position, _spawnPoint.transform.rotation, _spawnPoint).gameObject, 3f);
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
        var PlayerStatsIncreaseCoeff = GameManager.Instance.Player.GetComponent<PlayerController>().PlayerStatsIncreaseCoeff;
        activeWeapon.CurrentBar += Time.deltaTime / activeWeapon.ReloadDelay * PlayerStatsIncreaseCoeff;
        activeWeapon.BarIsNotEmpty = false;
        if (activeWeapon.CurrentBar >= activeWeapon.BarCapacity)
        {
            activeWeapon.BarIsNotEmpty = true;
        }
    }
}