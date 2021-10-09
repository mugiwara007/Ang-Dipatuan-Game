using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyLocomotionManger enemyLocomotionManger;
    EnemyAnimatorManager enemyAnimatorManager;
    public bool isPerformingAction;
    [Header("A.I Settings")]
    public float detectionRadius = 20;

    public EnemyAttackAction[] enemyAttacks;
    public EnemyAttackAction currentAttack;

    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;

    public float currentRecoveryTime = 0;

   private void Awake()
   {
        enemyLocomotionManger = GetComponent<EnemyLocomotionManger>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
    }

    private void Update()
    {
        HandleRecoveryTimer();
    }

    private void FixedUpdate()
   {
        HandleCurrentAction();
   }

    private void HandleCurrentAction()
   {
        if (enemyLocomotionManger.currentTarget != null)
        {
            enemyLocomotionManger.distanceFromTarget = Vector3.Distance(enemyLocomotionManger.currentTarget.transform.position, transform.position);
        }
        
        if (enemyLocomotionManger.currentTarget == null)
        {
            enemyLocomotionManger.HandleDetection();
        } 
        else if (enemyLocomotionManger.distanceFromTarget > enemyLocomotionManger.stoppingDistance)
        {
            enemyLocomotionManger.HandleMoveToTarget();
        }
        else if (enemyLocomotionManger.distanceFromTarget <= enemyLocomotionManger.stoppingDistance)
        {
            AttackTarget();
            enemyLocomotionManger.anim.SetBool("Run", false);
            
        }

    }

    #region Attacks

    private void AttackTarget()
    {
        if (isPerformingAction)
            return;

        if (currentAttack == null)
        {
            GetNewAttack();
        } else
        {
            Debug.Log("Attack");
            isPerformingAction = true;
            currentRecoveryTime = currentAttack.recoveryTime;
            enemyAnimatorManager.PlayTargetAnimation(currentAttack.actionAnimation, true);
            currentAttack = null;
        }
    }

    private void HandleRecoveryTimer()
    {
        if (currentRecoveryTime > 0)
        {
            currentRecoveryTime -= Time.deltaTime;
        }

        if (isPerformingAction)
        {
            if (currentRecoveryTime <= 0)
            {
                isPerformingAction = false;
            }
        }
    }

    private void GetNewAttack()
    {
        Vector3 targetDirection = enemyLocomotionManger.currentTarget.transform.position - transform.position;
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
        enemyLocomotionManger.distanceFromTarget = Vector3.Distance(enemyLocomotionManger.currentTarget.transform.position, transform.position);

        int maxScore = 0;

        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];

            if (enemyLocomotionManger.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyLocomotionManger.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    Debug.Log(maxScore);
                    maxScore += enemyAttackAction.attackScore;
                }
            }
        }

        
        int randomValue = Random.Range(0, maxScore);
        int temporaryScore = 0;

        
        for (int i = 0; i < enemyAttacks.Length; i++)
        {
            EnemyAttackAction enemyAttackAction = enemyAttacks[i];
            
            if (enemyLocomotionManger.distanceFromTarget <= enemyAttackAction.maximumDistanceNeededToAttack
                && enemyLocomotionManger.distanceFromTarget >= enemyAttackAction.minimumDistanceNeededToAttack)
            {
               
                if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                    && viewableAngle >= enemyAttackAction.minimumAttackAngle)
                {
                    if (currentAttack != null)
                        return;

                    temporaryScore += enemyAttackAction.attackScore;

                    if (temporaryScore > randomValue)
                    {
                        Debug.Log(enemyAttackAction);
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }

    }
    #endregion
}
