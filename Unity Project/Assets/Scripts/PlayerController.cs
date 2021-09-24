using UnityEngine;
using UnityEngine.Serialization;

namespace Game
{
    public class PlayerController : MonoBehaviour
    {
        public float speed = 1f;
        public float turnSmoothTime = 0.1f;
        public Animator animator;

        public Transform gameCamera;
        public float turnSmoothVelocity;

        public PlayerInput input;
        public void Update()
        {
            var direction = input.GetDirection();
            
            
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + gameCamera.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //    controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
        }
    }
}   