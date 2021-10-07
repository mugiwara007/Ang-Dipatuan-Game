using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : AnimatorManager
{
    EnemyLocomotionManger enemyLocomotionManger;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyLocomotionManger = GetComponentInParent<EnemyLocomotionManger>();
    }

    private void OnAnimatorMove()
    {
        float delta = Time.deltaTime;
        enemyLocomotionManger.enemyRigidbody.drag = 0;
        Vector3 deltaPosition = anim.deltaPosition;
        deltaPosition.y = 0;
        Vector3 velocity = deltaPosition / delta;
        enemyLocomotionManger.enemyRigidbody.velocity = velocity;
    }
}
