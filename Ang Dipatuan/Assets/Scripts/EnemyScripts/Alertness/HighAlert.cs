using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighAlert : MonoBehaviour
{
    private bool PlayerDetected = false;

    MainCharacterController playerScript;

    EnemyManager enemyScript;

    Animator playerAnim;

    private void Awake()
    {
        PlayerDetected = false;
        enemyScript = gameObject.GetComponentInParent<EnemyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerDetected)
        {
            playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
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

                enemyScript.detectionRadius = 40;
            }
        }
        else
        {

            enemyScript.maximumDetectionAngle = 65;
            enemyScript.minimumDetectionAngle = -65;

            enemyScript.detectionRadius = 40;
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
