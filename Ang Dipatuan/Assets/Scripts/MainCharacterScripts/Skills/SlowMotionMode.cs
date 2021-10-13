using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotionMode : MonoBehaviour
{
    GameObject[] enemies;
    Image skillColorYellow;

    private bool onCooldown;

    private void Awake()
    {
        skillColorYellow = GameObject.FindGameObjectWithTag("SlowMoSkillYellowImage").GetComponent<Image>();
        onCooldown = false;
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

        foreach (GameObject enemy in enemies)
        {
            Animator enemyAnim = enemy.GetComponent<Animator>();

            enemyAnim.SetFloat("animationSpeed", 1f);
        }
    }
}
