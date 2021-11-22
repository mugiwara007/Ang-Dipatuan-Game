using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialScript : MonoBehaviour
{
    QuestUI quest;
    public GameObject move, run, crouch, jump, normalAtk, heavyAtk, jumpAtk, block;
    public bool tctr;
    private float timer = 0f;
    int ctr = 1;

    private void Awake()
    {
        timer = 0f;
        tctr = true;
    }

    private void Update()
    {
        if(tctr == true)
        {
            timer += Time.deltaTime;
            if (timer > 3f && ctr == 1)
            {
                move.SetActive(true);
                if (timer > 9f && ctr == 1)
                {
                    move.SetActive(false);
                    ctr = 2;
                    timer = 0f;
                }
            }
            if (timer > 2f && ctr == 2)
            {
                run.SetActive(true);
                if (timer > 9f && ctr == 2)
                {
                    run.SetActive(false);
                    ctr = 3;
                    timer = 0f;
                }
            }
            if (timer > 2f && ctr == 3)
            {
                crouch.SetActive(true);
                if (timer > 9f && ctr == 3)
                {
                    crouch.SetActive(false);
                    ctr = 4;
                    timer = 0f;
                }
            }
            if (timer > 2f && ctr == 4)
            {
                jump.SetActive(true);
                if (timer > 9f && ctr == 4)
                {
                    jump.SetActive(false);
                    ctr++;
                    timer = 0f;
                }
            }
            if (timer > 2f && ctr == 5)
            {
                normalAtk.SetActive(true);
                if (timer > 9f && ctr == 5)
                {
                    normalAtk.SetActive(false);
                    ctr++;
                    timer = 0f;
                }
            }
            if (timer > 2f && ctr == 6)
            {
                heavyAtk.SetActive(true);
                if (timer > 9f && ctr == 6)
                {
                    heavyAtk.SetActive(false);
                    ctr++;
                    timer = 0f;
                }
            }
            if (timer > 2f && ctr == 7)
            {
                jumpAtk.SetActive(true);
                if (timer > 9f && ctr == 7)
                {
                    jumpAtk.SetActive(false);
                    ctr++;
                    timer = 0f;
                }
            }
            if (timer > 2f && ctr == 8)
            {
                block.SetActive(true);
                if (timer > 9f && ctr == 8)
                {
                    block.SetActive(false);
                    ctr++;
                    timer = 0f;
                }
            }
            if (ctr == 9)
            {
                tctr = false;
            }
        }
    }
}
