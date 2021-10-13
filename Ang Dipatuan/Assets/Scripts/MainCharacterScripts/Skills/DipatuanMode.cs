using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DipatuanMode : MonoBehaviour
{
    Image skillColorYellow;

    private bool onCooldown;

    DamageEnemy damageEnemyScript;

    GameObject fireOnSword1, fireOnSword2;

    private bool fireTurnOff = true;

    private void Awake()
    {
        damageEnemyScript = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();
        skillColorYellow = GameObject.FindGameObjectWithTag("DipatuanSkillYellowImage").GetComponent<Image>();

        fireOnSword1 = GameObject.FindGameObjectWithTag("KampilanArmed").transform.GetChild(4).gameObject;

        fireOnSword2 = GameObject.FindGameObjectWithTag("KampilanArmed").transform.GetChild(5).gameObject;
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {


        if (fireTurnOff)
        {
            fireOnSword1.SetActive(false);
            fireOnSword2.SetActive(false);
        }

        if (Input.GetButtonDown("DipatuanMode") && onCooldown == false)
        {
            damageEnemyScript.isDipatuanModeActivated = true;

            fireOnSword1.SetActive(true);
            fireOnSword2.SetActive(true);

            fireTurnOff = false;

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
        yield return new WaitForSeconds(7f);

        damageEnemyScript.isDipatuanModeActivated = false;

        fireOnSword1.SetActive(false);
        fireOnSword2.SetActive(false);

        fireTurnOff = true;
    }

}
