using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    Animator playerAnimator;

    PlayerBar playerBar;

    MainCharacterController charControl;

    void Awake()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        playerBar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();

        charControl = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Die
        PlayerDeath();
    }

    public void PlayerDeath()
    {
        if (playerBar.health <= 0)
        {
            Debug.Log("Deeath");
            playerAnimator.SetBool("isAlive", false);
            charControl.enabled = false;
            gameObject.GetComponent<Death>().enabled = false;
        }
        else
        {
            playerAnimator.SetBool("isAlive", true);
        }
       
    }

}
