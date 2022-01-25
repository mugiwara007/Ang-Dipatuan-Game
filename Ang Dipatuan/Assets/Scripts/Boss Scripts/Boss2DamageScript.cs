using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2DamageScript : MonoBehaviour
{
    Animator playerAnimator;
    MainCharacterController playerControl;
    PlayerBar playerHealth;

    BraveryMode isBraveryModeActivated;

    void Awake()
    {
        isBraveryModeActivated = GameObject.FindGameObjectWithTag("Player").GetComponent<BraveryMode>();
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerHealth = other.gameObject.GetComponent<PlayerBar>();
            playerControl = other.gameObject.GetComponent<MainCharacterController>();


            if (!isBraveryModeActivated.isBraveryModeActivated)
            {
                playerControl.AttackReset();

                //Check when player is blocking or not
                if (!playerControl.isBlocking)
                {
                    //When player is Crouching and get hit
                    playerControl.canCrouch = false;

                    var currentWeight = playerAnimator.GetLayerWeight(playerAnimator.GetLayerIndex("Crouch"));
                    playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("Crouch"), 0f);

                    //Do Player Damage Animation
                    playerAnimator.SetTrigger("isDamage");

                    //damage player
                    playerHealth.Damage(12f);

                    //Set boolean of taking damage to true so that player cant do anything when Taking damage
                    playerControl.isTakingDamage = true;
                }
                else
                {
                    //damage player
                    playerHealth.Damage(6f);
                }


                StartCoroutine("setTakingDamageFalse");
            }
        }
    }

    IEnumerator setTakingDamageFalse()
    {
        yield return new WaitForSeconds(1f);
        playerControl.isTakingDamage = false;
        playerControl.canCrouch = true;

    }
}
