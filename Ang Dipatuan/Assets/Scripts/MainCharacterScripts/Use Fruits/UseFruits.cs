using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseFruits : MonoBehaviour
{
    Text notEnough;

    Inventory inventory;

    PlayerBar playerStats;

    SaveQuestScript saveQuestScript;

    // Start is called before the first frame update
    void Start()
    {
        notEnough.enabled = false;
    }

    private void Awake()
    {
        notEnough = GameObject.FindGameObjectWithTag("NotEnoughFruits").GetComponent<Text>();
        inventory = gameObject.GetComponent<Inventory>();
        playerStats = gameObject.GetComponent<PlayerBar>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {   
            if(inventory.coconut <= 0)
            {
                notEnough.enabled = true;
                StartCoroutine("DisableNotEnough"); 
            }
            else
            {
                inventory.coconut -= 1;
                saveQuestScript.coconut -= 1;
                playerStats.Heal(16);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (inventory.pineapple <= 0)
            {
                notEnough.enabled = true;
                StartCoroutine("DisableNotEnough");
            }
            else
            {
                inventory.pineapple -= 1;
                saveQuestScript.pineapple -= 1;
                playerStats.enableMaxStamina();

                StartCoroutine("DisablePineappleEffect");
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (inventory.avocado <= 0)
            {
                notEnough.enabled = true;
                StartCoroutine("DisableNotEnough");
            }
            else
            {
                inventory.avocado -= 1;
                saveQuestScript.avocado -= 1;
                playerStats.AddMana(17);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (inventory.fruitbasket <= 0)
            {
                notEnough.enabled = true;
                StartCoroutine("DisableNotEnough");
            }
            else
            {
                inventory.fruitbasket -= 1;
                saveQuestScript.basket -= 1;
                playerStats.MaxManaHealth();
            }
        }
    }

    IEnumerator DisableNotEnough()
    {
        yield return new WaitForSeconds(0.5f);
        notEnough.enabled = false;
    }

    IEnumerator DisablePineappleEffect()
    {
        yield return new WaitForSeconds(5f);
        playerStats.disableMaxStamina();
    }
}
