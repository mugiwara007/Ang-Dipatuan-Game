using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //kunin dapat dito yung current quest
    public float health;
    public float mana;

    public int playerGold;

    public int currentQuest;
    public int currentQuest2;

    public int coconut;
    public int avocado;
    public int pineapple;
    public int fruitBasket;

    public bool armor1;
    public bool armor2;
    public bool armor3;

    public float[] position;

    //plus lahat ng data na kailangan isave dapat instantiate dito
    

    public PlayerData(PlayerBar player, Gold gold, SaveQuestScript updater, SaveQuestScript2 updater2, Inventory inventory, ClotheinInventory clothes)
    {
        //Player Stats
        health = player.health;
        mana = player.mana;

        //Gold
        playerGold = gold.total_gold;

        //Current Quest
        try
        {
           currentQuest = updater.CurrQuest;
        }
        catch
        {
            currentQuest = 3;
        }
        
        currentQuest2 = updater2.CurrQuest2;

        //Items
        coconut = inventory.coconut;
        avocado = inventory.avocado;
        pineapple = inventory.pineapple;
        fruitBasket = inventory.fruitbasket;

        //Armor
        armor1 = clothes.boughtCloth1;
        armor2 = clothes.boughtCloth2;
        armor3 = clothes.boughtCloth3;

        //create a float array para dito ma store yung Vector 3 position
        position = new float[3];

        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
