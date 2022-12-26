using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

    private Vector3 tempScale;

    private int currentAnimation;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        if(currentAnimation == Animator.StringToHash(animationName))
            return;

        anim.Play(animationName);

        currentAnimation = Animator.StringToHash(animationName);
    }

    public void SetFacingDirection(bool faceRight)
    {
        tempScale = transform.localScale;

        if(faceRight)
            tempScale.x = 1f;
        else
            tempScale.x = -1f;

        transform.localScale = tempScale;
    }
}
