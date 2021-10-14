using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveryMode : MonoBehaviour
{
    Image skillColorYellow;

    private bool onCooldown;

    public bool isBraveryModeActivated;

    Animator anim;

    GameObject forceField;

    MainCharacterController charControl;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        charControl = gameObject.GetComponent<MainCharacterController>();

        isBraveryModeActivated = false;
        skillColorYellow = GameObject.FindGameObjectWithTag("BraverySkillYellowImage").GetComponent<Image>();
        forceField = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("BraveryMode") && onCooldown == false)
        {
            isBraveryModeActivated = true;

            skillColorYellow.fillAmount = 0;
            onCooldown = true;

            //Activate animation for bravery mode
            anim.SetTrigger("braveryMode");

            //Apply ForceField
            forceField.SetActive(true);

            //Stops ACtion of dipatuan when performing animation
            charControl.enabled = false;

            //Allow Dipatuan to move again
            StartCoroutine("moveAgain");

            //turn off bravery mode
            StartCoroutine("braveryModeTurnOff");
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

    IEnumerator braveryModeTurnOff()
    {
        yield return new WaitForSeconds(5f);
        forceField.SetActive(false);
        isBraveryModeActivated = false;

    }

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(1f);
        charControl.enabled = true;

    }
}
