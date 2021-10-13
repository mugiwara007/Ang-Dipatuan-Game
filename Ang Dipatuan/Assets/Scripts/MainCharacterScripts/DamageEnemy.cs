using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    EnemyStats enemyStats;
    public float dmgPts;

    Animator enemyAnimator;

    public bool isDipatuanModeActivated;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();

            enemyAnimator = other.gameObject.GetComponent<Animator>();

            enemyAnimator.SetTrigger("hit");

            enemyStats.Damage(dmgPts);
       
        }
    }

    public void LightAtkDmg()
    {
        if (!isDipatuanModeActivated)
        {
            dmgPts = 15f;
        }
       
    }

    public void HeavyAtkDmg()
    {
        if (!isDipatuanModeActivated)
        {
            dmgPts = 25f;
        }
    }

    public void JumpAtkDmg()
    {
        if (!isDipatuanModeActivated)
        {
            dmgPts = 20f;
        }
    }

    public void StealthAtkDmg()
    {
          dmgPts = 100f;
    }
}
