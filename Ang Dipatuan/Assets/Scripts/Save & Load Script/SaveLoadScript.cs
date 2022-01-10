using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadScript : MonoBehaviour
{
    GameObject canvas;
    private bool canSave;

    private float timer = 0f;

    PlayerBar player;
    Gold gold;
    SaveQuestScript updater;
    Inventory inventory;
    ClotheinInventory clothes;
    SaveQuestScript saveQuestScript;
    GameSceneScript2 gameSceneScript2;

    private void Awake()
    {
        canvas = gameObject.transform.parent.Find("Canvas").gameObject;
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
      
        gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
        
        canSave = false;
        canvas.SetActive(false);
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<Gold>();
        updater = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        clothes = GameObject.FindGameObjectWithTag("Player").GetComponent<ClotheinInventory>();

        timer += Time.deltaTime;
        if (canSave && timer >= 10)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                SaveSystem.SavePlayer(player, gold, updater, inventory, clothes);
                timer = 0;
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                timer = 0;
                saveQuestScript.isLoadActive = true;
                canSave = false;
                canvas.SetActive(false);
                PlayerData data = SaveSystem.LoadPlayer();
                saveQuestScript.isLoadActive = true;
                gameSceneScript2.FadeToScene(data.sceneIndex);
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
}
