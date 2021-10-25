using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseShop : MonoBehaviour
{
    GameObject shopUI;

    BuyClothe BuyButton;

    private void Awake()
    {
        BuyButton = gameObject.transform.parent.Find("Buy").gameObject.GetComponent<BuyClothe>();
    }

    private void Update()
    {
        
    }

    public void CloseShoponCLick()
    {
        shopUI = GameObject.FindGameObjectWithTag("ShopUI");

        shopUI.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;

        BuyButton.clotheToBeBought = "";

    }

}
