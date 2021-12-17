using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighAlertForRifle : MonoBehaviour
{
    private bool PlayerDetected = false;

    MainCharacterController playerScript;
    Animator playerAnim;

    EnemyManager enemyScript;

    private void Awake()
    {
        PlayerDetected = false;
        enemyScript = gameObject.GetComponentInParent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        if (PlayerDetected)
        {
            if (playerScript.isCrouching == false && playerAnim.GetBool("isWalking"))
            {


                enemyScript.maximumDetectionAngle = 360;
                enemyScript.minimumDetectionAngle = -360;

                enemyScript.detectionRadius = 100;
            }
            else
            {
                enemyScript.maximumDetectionAngle = 65;
                enemyScript.minimumDetectionAngle = -65;

                enemyScript.detectionRadius = 70;
            }
        }
        else
        {

            enemyScript.maximumDetectionAngle = 65;
            enemyScript.minimumDetectionAngle = -65;

            enemyScript.detectionRadius = 70;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerScript = other.GetComponent<MainCharacterController>();
            PlayerDetected = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerScript = other.GetComponent<MainCharacterController>();
            PlayerDetected = false;
        }
    }


}
