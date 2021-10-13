using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator anim;

    public float delayToAttack = 0.2f;

    public void PlayTargetAnimation(string  targetAnim, bool isInteracting)
    {
        anim.applyRootMotion = isInteracting;
        anim.SetBool("isInteracting", isInteracting);
        anim.CrossFade(targetAnim, delayToAttack);

    }
}
