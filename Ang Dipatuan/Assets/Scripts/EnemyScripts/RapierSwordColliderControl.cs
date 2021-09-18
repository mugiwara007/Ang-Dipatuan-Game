using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RapierSwordColliderControl : MonoBehaviour
{
    GameObject enemyRapierSword;

    void Awake()
    {
        enemyRapierSword = GameObject.FindGameObjectWithTag("EnemyRapierSword");
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
