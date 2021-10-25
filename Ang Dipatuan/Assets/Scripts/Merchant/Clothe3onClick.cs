using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothe3onClick : MonoBehaviour
{
    BuyClothe BuyButton;


    private void Awake()
    {
        BuyButton = gameObject.transform.parent.Find("Buy").gameObject.GetComponent<BuyClothe>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clothe3ClickShop()
    {
        BuyButton.clotheToBeBought = "clothe3";
    }
}
