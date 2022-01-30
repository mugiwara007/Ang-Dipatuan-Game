using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int pineapple = 0, fruitbasket = 0, avocado = 0, coconut = 0;

    Text pineappleCounter, avocadoCounter, fruitbasketCounter, coconutCounter;
    SaveQuestScript saveQuestScript;

    private void Awake()
    {
        pineappleCounter = GameObject.FindGameObjectWithTag("PineappleCounter").GetComponent<Text>();
        avocadoCounter = GameObject.FindGameObjectWithTag("AvocadoCounter").GetComponent<Text>();
        fruitbasketCounter = GameObject.FindGameObjectWithTag("FruitBasketCounter").GetComponent<Text>();
        coconutCounter = GameObject.FindGameObjectWithTag("CoconutCounter").GetComponent<Text>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();

        coconut = saveQuestScript.coconut;
        avocado = saveQuestScript.avocado;
        fruitbasket = saveQuestScript.basket;
        pineapple = saveQuestScript.pineapple;
    }

    // Update is called once per frame
    void Update()
    {
        pineappleCounter.text = pineapple.ToString();
        avocadoCounter.text = avocado.ToString();
        fruitbasketCounter.text = fruitbasket.ToString();
        coconutCounter.text = coconut.ToString();
    }
}
