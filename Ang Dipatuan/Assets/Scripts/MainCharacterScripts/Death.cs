using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    Animator playerAnimator;

    PlayerBar playerBar;

    MainCharacterController charControl;

    GameObject followCamera;

    //Sound FX Variables
    public AudioSource DeathFX;

    void Awake()
    {
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();

        playerBar = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();

        charControl = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();

        followCamera = GameObject.FindGameObjectWithTag("followCamera");
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
            playerAnimator.SetBool("isAlive", false);
            charControl.enabled = false;

            if (!DeathFX.isPlaying)
            {
                DeathFX.Play();
            }

            gameObject.GetComponent<UseFruits>().enabled = false;
            gameObject.GetComponent<DipatuanMode>().enabled = false;
            gameObject.GetComponent<BraveryMode>().enabled = false;
            gameObject.GetComponent<SlowMotionMode>().enabled = false;

            //Makes Camera to go down a little bit when Player Dies
            Vector3 NewPos = new Vector3(followCamera.transform.localPosition.x, 0.35f, followCamera.transform.localPosition.z);
            followCamera.transform.localPosition = Vector3.Lerp(followCamera.transform.localPosition, NewPos, 8f * Time.deltaTime);

            gameObject.GetComponent<Death>().enabled = false;
        }
        else
        {
            playerAnimator.SetBool("isAlive", true);
            DeathFX.Stop();
        }
       
    }

}
