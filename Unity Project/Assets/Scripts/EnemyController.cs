using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    Transform Player;
    public float moveSpeed = 0f;
    public float rotationSpeed = 3.0f;

    public Animator animator;
    protected float movement;
    protected int movementHash;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        animator = GetComponent<Animator>();
        movementHash = Animator.StringToHash("speed");
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Player.position - transform.position), rotationSpeed * Time.deltaTime);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        animator.SetFloat(movementHash, 1);
    }
}