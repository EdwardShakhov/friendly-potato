using Player;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Condition")]
    public EnemyHealthBar EnemyHealthBar;
    [SerializeField] private int _enemyMaxHealth = 100;
    [SerializeField] private int _enemyHealth;
    [SerializeField] private bool _isDead; 
    public bool IsDead => _isDead;

    [Header("Enemy Behavior")]
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _enemyChasingDistance;
    private Transform _playerToChase;
    private readonly float _enemyAttackDistance = 1.3f;
    private readonly int _movementHash = Animator.StringToHash("speed");

    [Header("Enemy SFX")]
    [SerializeField] private ParticleSystem _bloodDeathSfx;
    [SerializeField] private ParticleSystem _bloodSfx;
    [SerializeField] private ParticleSystem _instantiateSfx;
    public ParticleSystem BloodSfx => _bloodSfx;

    protected void Awake()
    {
        Destroy(Instantiate(_instantiateSfx, transform.position, Quaternion.identity).gameObject, 3f);

        _playerToChase = _playerToChase ? _playerToChase : GameManager.Instance.Player.transform;
        _enemyAnimator = _enemyAnimator ? _enemyAnimator : GetComponent<Animator>();
        _enemyChasingDistance = Random.Range(_enemyAttackDistance, GameManager.Instance.MapSize);
        EnemyHealthBar.Hide();
        _enemyHealth = _enemyMaxHealth;
        _isDead = false;
        EnemyHealthBar.SetMaxHealth(_enemyMaxHealth);
    }

    protected void Start()
    {
        if (Vector3.Distance(GameManager.Instance.Player.transform.position, gameObject.transform.position) 
            < gameObject.GetComponent<EnemySound>().SoundDistanceToPlayer)
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
        var _distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        
        if (_distanceToPlayer > _enemyAttackDistance &&
            _distanceToPlayer < _enemyChasingDistance && !_isDead)
            //if distance to player between measures - chase
        {
            var transform1 = transform;
            transform1.position += transform1.forward * (_enemyMoveSpeed * Time.deltaTime);
            _enemyAnimator.SetFloat(_movementHash, 1);
        }
        else if (_distanceToPlayer <= _enemyAttackDistance && !_isDead)
            //if distance to player is small - attack and don't move
        {
            _enemyAnimator.SetFloat(_movementHash, 0);
            _enemyAnimator.SetBool("attack", true);
        }
        else         
            //else don't move
        {
            _enemyAnimator.SetFloat(_movementHash, 0);
        }
    }

    public void PlayerHit()
    //triggering by animation event
    {
        GameManager.Instance.Player.GetComponent<PlayerController>().DamagePlayer(Random.Range(15, 35));
    }
    
    public void DamageEnemy(int damage)
    {
        _enemyHealth -= damage;
        _enemyAnimator.SetBool("damage", true);
        if (_enemyHealth <= 0)
        {
            _isDead = true;
            Destroy(Instantiate(_bloodDeathSfx, transform.position, transform.rotation).gameObject, 3f);
            _enemyAnimator.SetFloat(_movementHash, 0);
            _enemyAnimator.SetBool("death", true);
            Destroy(gameObject, 7.0f);
            GameManager.Instance.Player.GetComponent<PlayerController>().PlayerExperience += Random.Range(15, 30);
            GameManager.Instance.CurrentNumberOfEnemiesOnMap--;
        }
    }
}