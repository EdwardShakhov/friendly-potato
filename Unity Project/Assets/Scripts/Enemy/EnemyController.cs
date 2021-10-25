using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _enemy;
    [SerializeField] private Animator _enemyAnimator;
    [SerializeField] private float _enemyMoveSpeed;
    [SerializeField] private float _enemyRotationSpeed;
    private int _movementHash;

    protected void Start()
    {
        _enemy = GameObject.FindGameObjectWithTag("Player").transform;

        _enemyAnimator = GetComponent<Animator>();
        _movementHash = Animator.StringToHash("speed");
    }

    protected void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_enemy.position - transform.position), _enemyRotationSpeed * Time.deltaTime);
        transform.position += transform.forward * (_enemyMoveSpeed * Time.deltaTime);

        _enemyAnimator.SetFloat(_movementHash, 1);
    }
}