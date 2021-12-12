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
    public bool IsDead => _isDead;

    [Header("Enemy Behavior")]
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _enemyChasingDistance;
    private Transform _playerToChase;
    private const float _enemyAttackDistance = 1.3f;

    [SerializeField] private Animator _enemyAnimator;
    private static readonly int _speedHash = Animator.StringToHash("speed");
    private static readonly int _attackHash = Animator.StringToHash("attack");
    private static readonly int _damageHash = Animator.StringToHash("damage");
    private static readonly int _deathHash = Animator.StringToHash("death");

    [Header("Enemy SFX")]
    [SerializeField] private ParticleSystem _bloodDeathSfx;
    [SerializeField] private ParticleSystem _bloodSfx;
    [SerializeField] private ParticleSystem _instantiateSfx;
    private const float _destroySfxTime = 3f;
    public ParticleSystem BloodSfx => _bloodSfx;
    
    [Header("Enemy Loot")]
    [SerializeField] private GameObject _healthKit;
    [SerializeField] private float _dropChanceHealthKit = 0.5f;
    [SerializeField] private GameObject _pistolKit;
    [SerializeField] private float _dropChancePistolKit = 0.5f;
    [SerializeField] private GameObject _shotgunKit;
    [SerializeField] private float _dropChanceShotgunKit = 0.5f;

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

        var distanceToPlayer = 
            Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

        //if distance to player between measures - chase
        if (distanceToPlayer > _enemyAttackDistance && distanceToPlayer < _enemyChasingDistance && !_isDead)
        {
            var transform1 = transform;
            transform1.position += transform1.forward * (_enemyMoveSpeed * Time.deltaTime);
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

    //triggering by animation event
    public void PlayerHit()
    {
        GameManager.Instance.Player.GetComponent<PlayerController>().DamagePlayer(Random.Range(15, 36));
    }

    public void DamageEnemy(int damage)
    {
        _enemyHealth -= damage;
        _enemyAnimator.SetBool(_damageHash, true);
        if (_enemyHealth <= 0)
        {
            _isDead = true;
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            Destroy(Instantiate(_bloodDeathSfx, transform.position, transform.rotation).gameObject, _destroySfxTime);
            _enemyAnimator.SetFloat(_speedHash, 0);
            _enemyAnimator.SetBool(_deathHash, true);
            Destroy(gameObject, _destroyEnemyObjectTime);
            GameManager.Instance.Player.GetComponent<PlayerController>().PlayerExperience += Random.Range(15, 31);
            GameManager.Instance.CurrentNumberOfEnemiesOnMap--;
            
            DropLoot();
        }
    }

    private void DropLoot()
    {
        if (Random.value < _dropChanceHealthKit)
        { 
            Instantiate(_healthKit, 
                gameObject.transform.position + new Vector3(Random.Range(-1f, 1f),1,Random.Range(-1f, 1f)), 
                transform.rotation);
        }
            
        if (Random.value < _dropChancePistolKit)
        { 
            Instantiate(_pistolKit, 
                gameObject.transform.position + new Vector3(Random.Range(-1f, 1f),1,Random.Range(-1f, 1f)), 
                transform.rotation);
        }
            
        if (Random.value < _dropChanceShotgunKit)
        { 
            Instantiate(_shotgunKit, 
                gameObject.transform.position + new Vector3(Random.Range(-1f, 1f),1,Random.Range(-1f, 1f)), 
                transform.rotation);
        }
    }
}