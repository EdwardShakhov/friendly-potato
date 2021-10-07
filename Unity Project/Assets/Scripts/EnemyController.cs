using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private int _movementHash;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        _movementHash = Animator.StringToHash("speed");
    }

    protected void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * (moveSpeed * Time.deltaTime);

        animator.SetFloat(_movementHash, 1);
    }
}