using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotionMode : MonoBehaviour
{
    GameObject[] enemies;
    GameObject bwPost;
    Image skillColorYellow;

    private bool onCooldown;

    Animator anim;

    MainCharacterController charControl;

    private void Awake()
    {
        skillColorYellow = GameObject.FindGameObjectWithTag("SlowMoSkillYellowImage").GetComponent<Image>();
        bwPost = GameObject.FindGameObjectWithTag("NormalPost").transform.GetChild(0).gameObject;
        onCooldown = false;

        anim = gameObject.GetComponent<Animator>();
        charControl = gameObject.GetComponent<MainCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");


        if (Input.GetButtonDown("SlowMotionMode") && onCooldown == false)
        {
            foreach (GameObject enemy in enemies)
            {
                Animator enemyAnim = enemy.GetComponent<Animator>();

                enemyAnim.SetFloat("animationSpeed", 0.2f);
            }

            //Activate Black & White Effect
            bwPost.SetActive(true);
            

            //Activate animation for bravery mode
            anim.SetTrigger("slowmotionMode");

            //Stops ACtion of dipatuan when performing animation
            charControl.enabled = false;

            //Allow Dipatuan to move again
            StartCoroutine("moveAgain");

            skillColorYellow.fillAmount = 0;
            onCooldown = true;
            StartCoroutine("backToNormalSpeed");
        }

        if (onCooldown)
        {
            skillColorYellow.fillAmount = skillColorYellow.fillAmount += 0.02f * Time.deltaTime;

            if(skillColorYellow.fillAmount >= 1f)
            {
                onCooldown = false;
            }
        }


    }

    IEnumerator backToNormalSpeed()
    {
        yield return new WaitForSeconds(5f);

        bwPost.SetActive(false);

        foreach (GameObject enemy in enemies)
        {
            Animator enemyAnim = enemy.GetComponent<Animator>();

            enemyAnim.SetFloat("animationSpeed", 1f);
        }
    }

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(1f);
        charControl.enabled = true;

    }
}
