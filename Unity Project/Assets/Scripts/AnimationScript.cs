using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class AnimationScript : MonoBehaviour
    {
        Animator animator;
        public PlayerController playerController;
        protected float movement;
        protected int movementHash;

        public void Start()
        {
            animator = GetComponent<Animator>();
            //animator.speed = playerController.speed;
            movementHash = Animator.StringToHash("movement");
        }

        public void Update()
        {
            var direction = playerController.input.GetDirection();

            if (direction.magnitude >= 0.5f)
            {
                animator.SetFloat(movementHash, 1);
            }

            if (direction.magnitude < 0.5f)
            {
                animator.SetFloat(movementHash, 0);
            }
        }
    }
}