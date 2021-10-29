using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform _player;
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _chasingDistanceToPlayer = GameManager.Instance.ChasingDistanceToPlayer;
    private readonly float _attackDistanceToPlayer = GameManager.Instance.AttackDistanceToPlayer;
    private float _chasingPlayer;
    private readonly int _movementHash = Animator.StringToHash("speed");
    private readonly int _attackHash = Animator.StringToHash("attack");

    protected void Start()
    {
        _player = GameManager.Instance.Player.transform;
        _enemyAnimator = GetComponent<Animator>();
        _chasingDistanceToPlayer = Random.Range(_attackDistanceToPlayer, GameManager.Instance.MapSize);
    }

    protected void Update()
    {
        transform.LookAt(_player);
        _chasingPlayer = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);
        
        //if distance to player between measures - chase
        if (_chasingPlayer >= _attackDistanceToPlayer &&
            _chasingPlayer <= _chasingDistanceToPlayer)
        {
            var transform1 = transform;
            transform1.position += transform1.forward * (_enemyMoveSpeed * Time.deltaTime);
            _enemyAnimator.SetFloat(_movementHash, 1);
        }

        //if distance to player is small - attack
        else if (_chasingPlayer <= _attackDistanceToPlayer)
        {
            _enemyAnimator.SetBool(_attackHash, true);
        }
        
        //else don't move
        else
        {
            _enemyAnimator.SetFloat(_movementHash, 0);
        }
    }
}