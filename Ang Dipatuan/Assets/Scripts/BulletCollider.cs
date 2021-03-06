using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollider : MonoBehaviour
{
    PlayerBar playerStats;

    Animator playerAnim;

    MainCharacterController playerControl;

    BraveryMode isBraveryModeActivated;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroyBullet");
        isBraveryModeActivated = GameObject.FindGameObjectWithTag("Player").GetComponent<BraveryMode>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerStats = other.gameObject.GetComponent<PlayerBar>();
            playerAnim = other.gameObject.GetComponent<Animator>();

            playerControl = other.gameObject.GetComponent<MainCharacterController>();

            if (!isBraveryModeActivated.isBraveryModeActivated)
            {

                //When player is Crouching and get hit
                playerControl.canCrouch = false;

                var currentWeight = playerAnim.GetLayerWeight(playerAnim.GetLayerIndex("Crouch"));
                playerAnim.SetLayerWeight(playerAnim.GetLayerIndex("Crouch"), 0f);


                //Trigger Damage Animation to Player
                playerAnim.SetTrigger("isDamage");


            //Deduct 13 hitpoints from PLayer Health
            playerStats.Damage(11f);

            //Set boolean of taking damage to true so that player cant do anything when Taking damage
            playerControl.isTakingDamage = true;

            StartCoroutine("setTakingDamageFalse");
            }

        }
        
    }

    IEnumerator setTakingDamageFalse()
    {
        yield return new WaitForSeconds(1f);
        playerControl.isTakingDamage = false;
        playerControl.canCrouch = true;
        Destroy(gameObject);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
