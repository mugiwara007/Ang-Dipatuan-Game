using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapierSwordColliderControlEnemySoldier : MonoBehaviour
{
    GameObject enemyRapierSword;

    void Awake()
    {
        enemyRapierSword = 
            this.gameObject
            .transform.GetChild(2)
            .GetChild(0) // base hip
            .GetChild(1) // base waist
            .GetChild(0) // base spine 
            .GetChild(0) // base spine 02
            .GetChild(3) // base r clavicle
            .GetChild(0) // base r upperarm
            .GetChild(0) // base r forearm
            .GetChild(2) // R hand
            .GetChild(1) // Mid 1
            .GetChild(0) // Mid 2
            .GetChild(0) // Mid 3
            .GetChild(0) // Rapier
            .gameObject;
    }  

    public void ActivateRapierSwordCollider()
    {
        enemyRapierSword.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void DisableRapierSwordCollider()
    {
        enemyRapierSword.GetComponent<CapsuleCollider>().enabled = false;
    }
}
