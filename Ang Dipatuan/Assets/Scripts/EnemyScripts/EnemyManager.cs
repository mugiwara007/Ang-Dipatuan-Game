using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    EnemyLocomotionManger enemyLocomotionManger;
    public bool isPerformingAction;
    [Header("A.I Settings")]
    public float detectionRadius = 20;

    public float maximumDetectionAngle = 50;
    public float minimumDetectionAngle = -50;

   private void Awake()
   {
        enemyLocomotionManger = GetComponent<EnemyLocomotionManger>();
   }

   private void FixedUpdate()
   {
        HandleCurrentAction();
   }

    private void HandleCurrentAction()
   {
        if (enemyLocomotionManger.currentTarget == null)
        {
            enemyLocomotionManger.HandleDetection();
        } else
        {
            enemyLocomotionManger.HandleMoveToTarget();
        }
   }

}
