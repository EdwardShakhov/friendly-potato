using Game;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform gameCamera;
    [SerializeField] private Animator animator;
    [SerializeField] private float speed;
    [SerializeField] private float turnSmoothTime;
    private float _turnSmoothVelocity; 
    private int _movementHash;

    protected void Start()
    {
        animator = GetComponent<Animator>();
        _movementHash = Animator.StringToHash("speed");
    }

    protected void Update()
    {
        var direction = PlayerInput.GetDirection();
        if (direction.magnitude >= 0.5f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + gameCamera.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            var moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * (speed * Time.deltaTime));
        }
        animator.SetFloat(_movementHash, direction.magnitude);
    }
}