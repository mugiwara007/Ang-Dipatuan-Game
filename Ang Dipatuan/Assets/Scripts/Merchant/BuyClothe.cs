using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyClothe : MonoBehaviour
{
    public string clotheToBeBought = "";

    ClotheinInventory playerClothe;
    Gold playerGold;

    GameObject Clothe1Shop, Clothe2Shop, Clothe3Shop;
    SaveQuestScript saveQuestScript;

    Text NotEnoughGold;

    private void Awake()
    {
        playerClothe = GameObject.FindGameObjectWithTag("Player").GetComponent<ClotheinInventory>();
        playerGold = GameObject.FindGameObjectWithTag("Player").GetComponent<Gold>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();

        NotEnoughGold = GameObject.FindGameObjectWithTag("NotEnoughGold").GetComponent<Text>();


        NotEnoughGold.enabled = false;

        if (saveQuestScript.armor1 == true){
            Destroy(Clothe1Shop);
        }

        if (saveQuestScript.armor2 == true)
        {
            Destroy(Clothe2Shop);
        }

        if (saveQuestScript.armor3 == true)
        {
            Destroy(Clothe3Shop);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Clothe1Shop = GameObject.FindGameObjectWithTag("Clothe1Shop");
        Clothe2Shop = GameObject.FindGameObjectWithTag("Clothe2Shop");
        Clothe3Shop = GameObject.FindGameObjectWithTag("Clothe3Shop");
    }

    public void BuyClotheClick()
    {
        if(clotheToBeBought == "clothe1")
        {
            if(playerGold.total_gold >= 800)
            {
                playerGold.total_gold -= 800;
                playerClothe.boughtCloth1 = true;
                saveQuestScript.armor1 = true;
                Destroy(Clothe1Shop);
            }
            else
            {
                clotheToBeBought = "";
                NotEnoughGold.enabled = true;
                StartCoroutine("DisableText");
            }
        }
        if (clotheToBeBought == "clothe2")
        {
            if (playerGold.total_gold >= 1200)
            {
                playerGold.total_gold -= 1200;
                playerClothe.boughtCloth2 = true;
                saveQuestScript.armor2 = true;
                Destroy(Clothe2Shop);
            }
            else
            {
                clotheToBeBought = "";
                NotEnoughGold.enabled = true;
                StartCoroutine("DisableText");
            }
        }
        if (clotheToBeBought == "clothe3")
        {
            if (playerGold.total_gold >= 1500)
            {
                playerGold.total_gold -= 1500;
                playerClothe.boughtCloth3 = true;
                saveQuestScript.armor3 = true;
                Destroy(Clothe3Shop);
            }
            else
            {
                clotheToBeBought = "";
                NotEnoughGold.enabled = true;
                StartCoroutine("DisableText");
            }
        }
    }

    IEnumerator DisableText()
    {
        yield return new WaitForSeconds(0.5f);
        NotEnoughGold.enabled = false;
    }

}
