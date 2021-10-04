using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {

        public PlayerInput input;
        public CharacterController controller;
        public Transform gameCamera;

        public float speed = 0.1f;
        public float turnSmoothTime = 0.1f;
        public float turnSmoothVelocity;

        public Animator animator;
        protected float movement;
        protected int movementHash;

        public void Start()
        {
            animator = GetComponent<Animator>();
            //animator.speed = playerController.speed;
            movementHash = Animator.StringToHash("speed");
        }

        public void Update()
        {
            var direction = input.GetDirection();
            
            if (direction.magnitude >= 0.5f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + gameCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

                animator.SetFloat(movementHash, 1);
            }

            if (direction.magnitude < 0.5f)
            {
                animator.SetFloat(movementHash, 0);
            }
        }
    }
}   