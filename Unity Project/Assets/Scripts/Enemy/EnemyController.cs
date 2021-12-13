using System.Diagnostics;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Condition")]
    public EnemyHealthBar EnemyHealthBar;
    [SerializeField] private int _enemyMaxHealth = 100;
    [SerializeField] private int _enemyHealth;
    [SerializeField] private bool _isDead;
    private const float _destroyEnemyObjectTime = 7f;

    [Header("Enemy Behavior")]
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _enemyChasingDistance;
    [SerializeField] private float _enemyAttackDistance; //1.3f for Zombie, 10f for Spider
    [SerializeField] private Rigidbody _enemyProjectile;
    [SerializeField] private Transform _enemyProjectileSpawnPoint;
    private Transform _playerToChase;
    private Animator _enemyAnimator;
    private static readonly int _speedHash = Animator.StringToHash("speed");
    private static readonly int _attackHash = Animator.StringToHash("attack");
    private static readonly int _damageHash = Animator.StringToHash("damage");
    private static readonly int _deathHash = Animator.StringToHash("death");

    [Header("Enemy SFX")]
    [SerializeField] private ParticleSystem _bloodDeathSfx;
    [SerializeField] private ParticleSystem _bloodSfx;
    [SerializeField] private ParticleSystem _instantiateSfx;
    private const float _destroySfxTime = 3f;

    [Header("Loot From Enemy")]
    [SerializeField] private GameObject _healthKit;
    [SerializeField] private float _dropChanceHealthKit;
    [SerializeField] private GameObject _ammoPistolKit;
    [SerializeField] private float _dropChancePistolKit;
    [SerializeField] private GameObject _ammoShotgunKit;
    [SerializeField] private float _dropChanceShotgunKit;
    
    public bool IsDead => _isDead;
    public ParticleSystem BloodSfx => _bloodSfx;

    protected void Awake()
    {
        Destroy(Instantiate(_instantiateSfx, transform.position, Quaternion.identity).gameObject, _destroySfxTime);

        _playerToChase = _playerToChase ? _playerToChase : GameManager.Instance.Player.transform;
        _enemyAnimator = _enemyAnimator ? _enemyAnimator : GetComponent<Animator>();
        gameObject.GetComponent<Animator>().speed = Random.Range(0.9f, 1.2f);
        _enemyChasingDistance = Random.Range(_enemyAttackDistance, GameManager.Instance.MapSize);
        EnemyHealthBar.Hide();
        _enemyHealth = _enemyMaxHealth;
        _isDead = false;
        EnemyHealthBar.SetMaxHealth(_enemyMaxHealth);
        
        _healthKit = _healthKit ? _healthKit : GameManager.Instance.HealthKit;
        _ammoPistolKit = _ammoPistolKit ? _ammoPistolKit : GameManager.Instance.AmmoPistolKit;
        _ammoShotgunKit = _ammoShotgunKit ? _ammoShotgunKit : GameManager.Instance.AmmoShotgunKit;
    }

    protected void Start()
    {
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, gameObject.transform.position) <
            gameObject.GetComponent<EnemySound>().SoundDistanceToPlayer)
        {
            gameObject.GetComponent<EnemyController>().GetComponent<EnemySound>().Instantiation();
        }
    }

    protected void Update()
    {
        EnemyHealthBar.SetHealth(_enemyHealth);

        if (!_isDead)
        {
            transform.LookAt(_playerToChase);
        }

        EnemiesBehaviour();
        
    }

    private void EnemiesBehaviour()
    {
        var distanceToPlayer = 
            Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

        //if distance to player between measures - chase
        if (distanceToPlayer > _enemyAttackDistance && distanceToPlayer < _enemyChasingDistance && !_isDead)
        {
            transform.position += transform.forward * (_enemyMoveSpeed * Time.deltaTime);
            _enemyAnimator.SetFloat(_speedHash, 1);
        }
        //if distance to player is small - attack and don't move
        else if (distanceToPlayer <= _enemyAttackDistance && !_isDead)
        {
            _enemyAnimator.SetFloat(_speedHash, 0);
            _enemyAnimator.SetBool(_attackHash, true);
        }
        //else don't move
        else
        {
            _enemyAnimator.SetFloat(_speedHash, 0);
        }
    }
    
    //triggering by Zombie animation event
    public void PlayerHit()
    {
        GameManager.Instance.Player.GetComponent<PlayerController>().DamagePlayer(Random.Range(15, 36));
    }

    //triggering by Spider animation event
    public void PlayerShot()
    {
        if (_enemyAttackDistance > 2)
        {
            var instantiatedBullet = Instantiate(_enemyProjectile,
                _enemyProjectileSpawnPoint.transform.position, transform.rotation);
            instantiatedBullet.velocity = transform.TransformDirection(new Vector3(0, 0, 20f));
        }
    }

    public void DamageEnemy(int damage)
    {
        _enemyHealth -= damage;
        _enemyAnimator.SetBool(_damageHash, true);
        if (_enemyHealth <= 0)
        {
            _isDead = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(Instantiate(_bloodDeathSfx, transform.position, transform.rotation).gameObject, _destroySfxTime);
            _enemyAnimator.SetFloat(_speedHash, 0);
            _enemyAnimator.SetBool(_deathHash, true);
            Destroy(gameObject, _destroyEnemyObjectTime);
            GameManager.Instance.Player.GetComponent<PlayerController>().PlayerExperience += Random.Range(15, 31);

            switch (gameObject.name)
            {
                case "Zombie(Clone)":
                    GameManager.Instance.CurrentNumberOfZombies--;
                    break;
                case "Spider(Clone)":
                    GameManager.Instance.CurrentNumberOfSpiders--;
                    break;
            }

            DropLoot();
        }
    }

    private void DropLoot()
    {
        if (Random.value < _dropChanceHealthKit)
        { 
            Instantiate(_healthKit, 
                gameObject.transform.position + new Vector3(Random.Range(-1f, 1f),1f,Random.Range(-1f, 1f)), 
                transform.rotation);
        }
            
        if (Random.value < _dropChancePistolKit)
        { 
            Instantiate(_ammoPistolKit, 
                gameObject.transform.position + new Vector3(Random.Range(-1f, 1f),1f,Random.Range(-1f, 1f)), 
                transform.rotation);
        }
            
        if (Random.value < _dropChanceShotgunKit)
        { 
            Instantiate(_ammoShotgunKit, 
                gameObject.transform.position + new Vector3(Random.Range(-1f, 1f),1f,Random.Range(-1f, 1f)), 
                transform.rotation);
        }
    }
}