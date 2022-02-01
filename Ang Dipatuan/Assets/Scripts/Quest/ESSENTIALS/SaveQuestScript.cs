using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveQuestScript : MonoBehaviour
{
    public static SaveQuestScript Instance;

    public int CurrQuest;
    public int questPos;
    public int gold = 0;
    public bool isLoadActive = false;
    public bool secondQuest = false;
    public bool thirdQuest = false;
    public bool fourthQuest = false;
    public bool seventhQuest = false;
    public bool eightQuest = false;
    public bool lastQuest = false;

    public bool quest4Accepted = false;
    public bool quest5Accepted = false;
    public bool quest6Accepted = false;
    public bool quest7Accepted = false;

    public int coconut = 0;
    public int pineapple = 0;
    public int avocado = 0;
    public int basket = 0;

    public bool armor1 = false;
    public bool armor2 = false;
    public bool armor3 = false;

    Inventory inventory;

    void Awake()
    {
        try
        {
            inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        }
        catch
        {
            Debug.Log("Inventory Script not found.");
        }
        

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        try
        {
            coconut = inventory.coconut;
            pineapple = inventory.pineapple;
            avocado = inventory.avocado;
            basket = inventory.fruitbasket;
        } catch
        {
            //Ignore error
        }
    }
}
