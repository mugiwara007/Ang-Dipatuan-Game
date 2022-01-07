using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadScript : MonoBehaviour
{
    GameObject canvas;
    private bool canSave;

    GameObject UI;

    private float timer = 0f;

    PlayerBar player;
    Gold gold;
    SaveQuestScript updater;
    SaveQuestScript2 updater2;
    Inventory inventory;
    ClotheinInventory clothes;

    private void Awake()
    {
        canvas = gameObject.transform.parent.Find("Canvas").gameObject;
        canSave = false;
        canvas.SetActive(false);
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<Gold>();
        try
        {
            updater = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        }
        catch
        {
            updater = null;
        }
        
        updater2 = GameObject.FindGameObjectWithTag("Updater2").GetComponent<SaveQuestScript2>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        clothes = GameObject.FindGameObjectWithTag("Player").GetComponent<ClotheinInventory>();

        timer += Time.deltaTime;
        if (canSave && timer >= 10)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SaveSystem.SavePlayer(player, gold, updater, updater2, inventory, clothes);
                timer = 0;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                timer = 0;


                PlayerData data = SaveSystem.LoadPlayer();
                player.health = data.health;
                player.mana = data.mana;

                gold.total_gold = data.playerGold;

                try
                {
                  updater.CurrQuest = data.currentQuest;
                }
                catch
                {
                    Debug.Log("NASA MAINLAND TAYO");
                }
                
                updater2.CurrQuest2 = data.currentQuest2;

                inventory.coconut = data.coconut;
                inventory.avocado = data.avocado;
                inventory.pineapple = data.pineapple;
                inventory.fruitbasket = data.fruitBasket;

                clothes.boughtCloth1 = data.armor1;
                clothes.boughtCloth2 = data.armor2;
                clothes.boughtCloth3 = data.armor3;

                Vector3 position;

                position.x = data.position[0];
                position.y = data.position[1];
                position.z = data.position[2];

                StartCoroutine(activateCharController1(position));

                canSave = false;
                canvas.SetActive(false);

            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("PUMASOKK NNANAMN");
            canSave = true;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canSave = false;
            canvas.SetActive(false);
        }
    }

    IEnumerator activateCharController1(Vector3 pos)
    {
        //set this to false so that you can teleport the position of character
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;

    }
}
