using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CloseShop : MonoBehaviour
{
    GameObject shopUI;

    BuyClothe BuyButton;

    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;

    private void Awake()
    {
        BuyButton = gameObject.transform.parent.Find("Buy").gameObject.GetComponent<BuyClothe>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
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
        cinemachineBrain.enabled = true;
        movement.stun = false;
        BuyButton.clotheToBeBought = "";

    }

}
