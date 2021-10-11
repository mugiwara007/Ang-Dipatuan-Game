using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{
    EnemyStats enemyStats;
    public float dmgPts;

    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemyStats = other.gameObject.GetComponent<EnemyStats>();

            enemyStats.Damage(dmgPts);
       
        }
    }

    public void LightAtkDmg()
    {
        dmgPts = 15f;
    }

    public void HeavyAtkDmg()
    {
        dmgPts = 25f;
    }

    public void JumpAtkDmg()
    {
        dmgPts = 20f;
    }

    public void StealthAtkDmg()
    {
        dmgPts = 100f;
    }
}
