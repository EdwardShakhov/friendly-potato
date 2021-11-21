using Player;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Condition")]
    public EnemyHealthBar EnemyHealthBar;
    [SerializeField] private int _enemyMaxHealth = 1000;
    [SerializeField] private int _enemyHealth;
    [SerializeField] private bool _isDead;
    
    [Header("Enemy Behavior")]
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _enemyChasingDistance;
    private Transform _playerToChase;
    private readonly float _enemyAttackDistance = GameManager.EnemyAttackDistance;
    private readonly int _movementHash = Animator.StringToHash("speed");

    protected void Awake()
    {
        _playerToChase = _playerToChase ? _playerToChase : GameManager.Instance.Player.transform;
        _enemyAnimator = _enemyAnimator ? _enemyAnimator : GetComponent<Animator>();
        _enemyChasingDistance = Random.Range(_enemyAttackDistance, GameManager.Instance.MapSize);
        EnemyHealthBar.Hide();
        _enemyHealth = _enemyMaxHealth;
        _isDead = false;
        EnemyHealthBar.SetMaxHealth(_enemyMaxHealth);
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
            //GameManager.Instance.Player.GetComponent<PlayerController>().DamagePlayer(1);
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
        GameManager.Instance.Player.GetComponent<PlayerController>().DamagePlayer(Random.Range(150, 350));
    }
    
    public void DamageEnemy(int damage)
    {
        _enemyHealth -= damage;
        _enemyAnimator.SetBool("damage", true);
        if (_enemyHealth <= 0)
        {
            _isDead = true;
            _enemyAnimator.SetFloat(_movementHash, 0);
            _enemyAnimator.SetBool("death", true);
            Destroy(gameObject, 7.0f);
        }
    }
}