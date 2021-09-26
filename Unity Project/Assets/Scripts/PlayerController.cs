using UnityEngine;
using UnityEngine.Serialization;

namespace Game  //what for?
{
    public class PlayerController : MonoBehaviour
    {

        public PlayerInput input;
        public CharacterController controller;
        public Transform gameCamera;
        public Animator animator;

        public float speed = 0.1f;
        public float turnSmoothTime = 0.1f;
        public float turnSmoothVelocity;

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
            }
        }
    }
}   