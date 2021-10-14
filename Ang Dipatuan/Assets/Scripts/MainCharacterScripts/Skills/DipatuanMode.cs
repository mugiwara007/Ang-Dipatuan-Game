using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DipatuanMode : MonoBehaviour
{
    Image skillColorYellow;

    private bool onCooldown;

    DamageEnemy damageEnemyScript;

    GameObject fireOnSword1, fireOnSword2, ember;

    private bool fireTurnOff = true;

    Animator anim;

    MainCharacterController charControl;

    GameObject kampilan;

    private void Awake()
    {
        kampilan = GameObject.FindGameObjectWithTag("KampilanArmed");

        damageEnemyScript = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();
        skillColorYellow = GameObject.FindGameObjectWithTag("DipatuanSkillYellowImage").GetComponent<Image>();

        fireOnSword1 = GameObject.FindGameObjectWithTag("KampilanArmed").transform.GetChild(2).gameObject;

        fireOnSword2 = GameObject.FindGameObjectWithTag("KampilanArmed").transform.GetChild(3).gameObject;

        ember = GameObject.FindGameObjectWithTag("KampilanArmed").transform.GetChild(7).gameObject;

        onCooldown = false;

        anim = gameObject.GetComponent<Animator>();
        charControl = gameObject.GetComponent<MainCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {


        if (fireTurnOff)
        {
            fireOnSword1.SetActive(true);
            fireOnSword2.SetActive(false);
            ember.SetActive(false);
        }

        if (Input.GetButtonDown("DipatuanMode") && onCooldown == false)
        {
            damageEnemyScript.isDipatuanModeActivated = true;

            fireOnSword1.SetActive(false);
            fireOnSword2.SetActive(true);
            ember.SetActive(true);

            fireTurnOff = false;

            damageEnemyScript.dmgPts = 30f;

            skillColorYellow.fillAmount = 0;
            onCooldown = true;

            //Display kampilan on hand
            kampilan.SetActive(true);

            //Activate animation for bravery mode
            anim.SetTrigger("dipatuanMode");

            //Stops ACtion of dipatuan when performing animation
            charControl.enabled = false;

            //Allow Dipatuan to move again
            StartCoroutine("moveAgain");

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

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(1.3f);
        charControl.enabled = true;
    }
}
