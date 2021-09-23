using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalkingForward");
    }

    void Update()
    {
        bool isWalkingForward = animator.GetBool(isWalkingHash);
        bool press_WASD = Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("d") || Input.GetKey("s");

        if (!isWalkingForward && press_WASD)
        {
            animator.SetBool(isWalkingHash, true);
        }

        if (isWalkingForward && !press_WASD)
        {
            animator.SetBool(isWalkingHash, false);
        }
    }
}
