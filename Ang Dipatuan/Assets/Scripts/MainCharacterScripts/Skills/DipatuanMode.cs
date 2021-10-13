using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DipatuanMode : MonoBehaviour
{
    Image skillColorYellow;

    private bool onCooldown;

    DamageEnemy damageEnemyScript;

    private void Awake()
    {
        damageEnemyScript = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();
        skillColorYellow = GameObject.FindGameObjectWithTag("DipatuanSkillYellowImage").GetComponent<Image>();
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
       
        


        if (Input.GetButtonDown("DipatuanMode") && onCooldown == false)
        {
            damageEnemyScript.isDipatuanModeActivated = true;

            damageEnemyScript.dmgPts = 30f;

            skillColorYellow.fillAmount = 0;
            onCooldown = true;

            StartCoroutine("DipatuanModeOff");
        }

        if (onCooldown)
        {
            skillColorYellow.fillAmount = skillColorYellow.fillAmount += 0.02f * Time.deltaTime;

            if (skillColorYellow.fillAmount >= 1f)
            {
                onCooldown = false;
            }
        }


    }

    IEnumerator DipatuanModeOff()
    {
        yield return new WaitForSeconds(5f);

        damageEnemyScript.isDipatuanModeActivated = false;
    }

}
