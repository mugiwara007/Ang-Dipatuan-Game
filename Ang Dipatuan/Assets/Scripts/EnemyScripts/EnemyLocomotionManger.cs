using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

    public class EnemyLocomotionManger : MonoBehaviour
    {
        EnemyManager enemyManager;
    NavMeshAgent navMeshAgent;
    public Rigidbody enemyRigidbody;
        public CharacterStats currentTarget;
        public LayerMask detectionLayer;
    public Animator anim;

    public float distanceFromTarget;
    public float stoppingDistance = 5f;
    public float rotationSpeed = 15f;

        private void Awake()
        {
            enemyManager = GetComponent<EnemyManager>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        enemyRigidbody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //stoppingDistance = 5f;
        navMeshAgent.enabled = false;
        enemyRigidbody.isKinematic = false;
    }

    public void HandleDetection()
        {
            Collider[] collider = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

        for (int i = 0; i < collider.Length; i++)
        {
            CharacterStats characterStats = collider[i].transform.GetComponent<CharacterStats>();

            if (characterStats != null)
            {
                Vector3 targetDirection = characterStats.transform.position - transform.position;
                float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle)
                {
                    currentTarget = characterStats;
                }
            }
        }
    }

    public void HandleMoveToTarget()
    {
        if (enemyManager.isPerformingAction)
            return;

        Vector3 targetDirection = currentTarget.transform.position - transform.position;
        distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
        float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

        if (enemyManager.isPerformingAction)
        {
            navMeshAgent.enabled = false;
        }
        else
        {
            if (distanceFromTarget > stoppingDistance)
            {
                anim.SetBool("Run", true);
            }
            
            else if (distanceFromTarget <= stoppingDistance)
            {
                anim.SetBool("Run", false);
            }

        }


        if (distanceFromTarget >= 60)
        {
            anim.SetBool("Run", false);
            currentTarget = null;
        }

        

        HandleRotateTowardsTarget();

        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }

    public void HandleRotateTowardsTarget()
    {

        //Rotate Manually
        if (enemyManager.isPerformingAction)
        {
            Vector3 direction = currentTarget.transform.position - transform.position;
            direction.y = 0;
            direction.Normalize();

            if (direction == Vector3.zero)
            {
                direction = transform.forward;
            }

            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);
        } else
        {
            Vector3 relativeDirection = transform.InverseTransformDirection(navMeshAgent.desiredVelocity);
            Vector3 targetVelocity = enemyRigidbody.velocity;

            navMeshAgent.enabled = true;
            navMeshAgent.SetDestination(currentTarget.transform.position);
            enemyRigidbody.velocity = targetVelocity;
            transform.rotation = Quaternion.Slerp(transform.rotation, navMeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
        }

        navMeshAgent.transform.localPosition = Vector3.zero;
        navMeshAgent.transform.localRotation = Quaternion.identity;
    }
}