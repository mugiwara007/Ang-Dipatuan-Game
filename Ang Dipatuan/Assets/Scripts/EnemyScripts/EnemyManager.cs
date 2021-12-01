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

    PlayerBar charStats;

    private bool enemyPatrolling;

    private float timeToTurnAround = 0f;

    private bool turnAvail = false;

   private void Awake()
   {
        enemyLocomotionManger = GetComponent<EnemyLocomotionManger>();
        enemyAnimatorManager = GetComponent<EnemyAnimatorManager>();
        charStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();

        enemyPatrolling = true;

    }

    private void Update()
    {
        HandleRecoveryTimer();
    }

    public void RotateCharacter()
    {
        if (turnAvail)
        {
            gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, -gameObject.transform.localScale.z);
            turnAvail = false;
        }
        
    }
    public void CanTurn()
    {
        turnAvail = true;
    }

    private void FixedUpdate()
   {
        HandleCurrentAction();

        if(enemyPatrolling)
        {
            enemyLocomotionManger.anim.SetBool("isWalking", true);

            timeToTurnAround += Time.deltaTime;

            if(timeToTurnAround >= 7.2f)
            {
                //Humarap sa Likod yung Nag papatrol
                //gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, -gameObject.transform.localScale.z);

                enemyLocomotionManger.anim.SetTrigger("turnAround");
                timeToTurnAround = -7.8f;


            }

        }
        else
        {
            enemyLocomotionManger.anim.SetBool("isWalking", false);
        }
    }

    private void HandleCurrentAction()
   {
        if(charStats.health > 0)
        {
            //pag merong hinahabol kalaban
            if (enemyLocomotionManger.currentTarget != null)
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, gameObject.transform.localScale.y, Mathf.Abs(gameObject.transform.localScale.z));
                enemyLocomotionManger.distanceFromTarget = Vector3.Distance(enemyLocomotionManger.currentTarget.transform.position, transform.position);
                enemyPatrolling = false;
            }
        
            //Pag walang hinahabol kalaban
            if (enemyLocomotionManger.currentTarget == null)
            {
                enemyLocomotionManger.HandleDetection();
                enemyPatrolling = true;
            } 
            else if (enemyLocomotionManger.distanceFromTarget > enemyLocomotionManger.stoppingDistance)
            {
                enemyLocomotionManger.HandleMoveToTarget();
            }
            else if (enemyLocomotionManger.distanceFromTarget <= enemyLocomotionManger.stoppingDistance)
            {
                enemyLocomotionManger.anim.SetBool("Run", false);
                enemyLocomotionManger.HandleRotateTowardsTarget();
                AttackTarget();
            }
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
                        currentAttack = enemyAttackAction;
                    }
                }
            }
        }

    }
    #endregion
}
