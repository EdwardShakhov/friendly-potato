using Player;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int _enemyMaxHealth = 1000;
    [SerializeField] private int _enemyHealth;
    [SerializeField] private bool _isDead;
    
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _enemyChasingDistance;
    
    private Transform _playerToChase;
    private readonly float _enemyAttackDistance = GameManager.EnemyAttackDistance;
    private readonly int _movementHash = Animator.StringToHash("speed");

    protected void Awake()
    {
        _playerToChase = _playerToChase ? _playerToChase : GameManager.Instance.Player.transform;
        _enemyAnimator = _enemyAnimator ? _enemyAnimator : GetComponent<Animator>();
        _enemyChasingDistance = Random.Range(_enemyAttackDistance, GameManager.Instance.MapSize);
        
        _enemyHealth = _enemyMaxHealth;
        _isDead = false;
    }

    protected void Update()
    {
        if (!_isDead)
        {
            transform.LookAt(_playerToChase);
        }
        _distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        
        //if distance to player between measures - chase
        if (_distanceToPlayer >= _enemyAttackDistance &&
            _distanceToPlayer <= _enemyChasingDistance && !_isDead)
        {
            var transform1 = transform;
            transform1.position += transform1.forward * (_enemyMoveSpeed * Time.deltaTime);
            _enemyAnimator.SetFloat(_movementHash, 1);
        }

        //if distance to player is small - attack and don't move
        else if ((_distanceToPlayer <= _enemyAttackDistance) && !_isDead)
        {
            _enemyAnimator.SetFloat(_movementHash, 0);
            _enemyAnimator.SetBool("attack", true);
            PlayerController.DamagePlayer(1);
        }
        
        //else don't move
        else
        {
            _enemyAnimator.SetFloat(_movementHash, 0);
        }
    }

    public void DamageEnemy(int damage)
    {
        _enemyHealth -= damage;
        _enemyAnimator.SetBool("damage", true);
        if (_enemyHealth == 0)
        {
            _isDead = true;
            _enemyAnimator.SetFloat(_movementHash, 0);
            _enemyAnimator.SetBool("death", true);
            Destroy(gameObject, 7.0f);
        }
    }
}