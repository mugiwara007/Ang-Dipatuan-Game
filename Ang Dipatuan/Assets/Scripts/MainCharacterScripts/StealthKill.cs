using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthKill : MonoBehaviour
{
    GameObject KrisDagger;

    bool canStealthKill = false;

    GameObject stealthKillDetector, target;
    MainCharacterController player;

    void Awake()
    {
        KrisDagger = GameObject.FindGameObjectWithTag("KrisDagger");

        KrisDagger.gameObject.SetActive(false);

        player = gameObject.GetComponent<MainCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && canStealthKill)
        {
            
            //if crouching stand up first before doing stealth kill
            if (player.isCrouching)
            {
                player.canCrouch = false;

                StartCoroutine("StandUpFirst");
            }
            else
            {
                //Force the player to look at the enemy when doing stealth kill
                Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z); ;
                gameObject.transform.LookAt(targetPos);


                canStealthKill = false;

                //disable movement when doing stealth kill
                gameObject.GetComponent<MainCharacterController>().enabled = false;

                //when crouching and doing stealth kill, stand up first
                gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);

                //activate stealth kill animation
                gameObject.GetComponent<Animator>().SetTrigger("Stab");

                //show kris dagger game object
                KrisDagger.gameObject.SetActive(true);
            }

            StartCoroutine("DisableKrisSword");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("StealthKillDetector"))
        {
            stealthKillDetector = other.gameObject;
            canStealthKill = true;

            //get the position of "lookAtPosition" game object
            target = other.transform.GetChild(0).gameObject;
        }
    }

    IEnumerator DisableKrisSword()
    {
        yield return new WaitForSeconds(2f);
        KrisDagger.gameObject.SetActive(false);
        gameObject.GetComponent<MainCharacterController>().enabled = true;

        player.canCrouch = true;
    }

    IEnumerator StandUpFirst()
    {
        canStealthKill = false;

        yield return new WaitForSeconds(0.1f);

        //disable movement when doing stealth kill
        gameObject.GetComponent<MainCharacterController>().enabled = false;

        //Force the player to look at the enemy when doing stealth kill
        Vector3 targetPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z); ;
        gameObject.transform.LookAt(targetPos);


        //when crouching and doing stealth kill, stand up first
        gameObject.GetComponent<Animator>().SetLayerWeight(1, 0);

        //activate stealth kill animation
        gameObject.GetComponent<Animator>().SetTrigger("Stab");

        //show kris dagger game object
        KrisDagger.gameObject.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("StealthKillDetector"))
        {
            canStealthKill = false;
        }
    }
}
