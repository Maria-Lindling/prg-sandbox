using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationManager : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator    = GetComponent<Animator>();
        animator.SetBool("facingRight", true);
    }

     public void SetAnimatorValues(float moveX, float moveY)
    {
        if( moveX > 0 )
        {
            animator.SetBool("facingRight", true);
        }
        else if (moveX < 0)
        {
            animator.SetBool("facingRight", false);
        }

        animator.SetBool("isMoving", moveX != 0.0f || moveY != 0.0f);
    }

}
