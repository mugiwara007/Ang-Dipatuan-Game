using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OpenInventory : MonoBehaviour
{
    GameObject Inventory;

    private bool inventoryOpen = false;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Inventory");

        Inventory.SetActive(inventoryOpen);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryOpen = !inventoryOpen;
            Inventory.SetActive(inventoryOpen);
            Cursor.visible = inventoryOpen;
            Cursor.lockState = CursorLockMode.Confined;
        }

        if(inventoryOpen == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
}
