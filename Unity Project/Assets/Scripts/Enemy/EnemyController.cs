using Player;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _distanceToPlayer;
    [SerializeField] private float _enemyChasingDistance;
    private readonly float _enemyAttackDistance = GameManager.EnemyAttackDistance;
    private readonly int _movementHash = Animator.StringToHash("speed");
    private readonly int _attackHash = Animator.StringToHash("attack");

    protected void Start()
    {
        _player = GameManager.Instance.Player.transform;
        _enemyAnimator = GetComponent<Animator>();
        _enemyChasingDistance = Random.Range(_enemyAttackDistance, GameManager.Instance.MapSize);
    }

    protected void Update()
    {
        transform.LookAt(_player);
        _distanceToPlayer = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        
        //if distance to player between measures - chase
        if (_distanceToPlayer >= _enemyAttackDistance &&
            _distanceToPlayer <= _enemyChasingDistance)
        {
            var transform1 = transform;
            transform1.position += transform1.forward * (_enemyMoveSpeed * Time.deltaTime);
            _enemyAnimator.SetFloat(_movementHash, 1);
        }

        //if distance to player is small - attack and don't move
        else if (_distanceToPlayer <= _enemyAttackDistance)
        {
            _enemyAnimator.SetFloat(_movementHash, 0);
            _enemyAnimator.SetBool(_attackHash, true);
            DamageSystem.DamagePlayer(1);
        }
        
        //else don't move
        else
        {
            _enemyAnimator.SetFloat(_movementHash, 0);
        }
    }
}