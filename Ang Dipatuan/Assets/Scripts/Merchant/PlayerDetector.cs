using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerDetector : MonoBehaviour
{
    GameObject canvas;
    private bool canShop;

    GameObject shopUI;
    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;

    private void Awake()
    {
        canvas = gameObject.transform.parent.Find("Canvas").gameObject;
        canShop = false;

        shopUI = GameObject.FindGameObjectWithTag("ShopUI");
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
    }

    private void Start()
    {
        shopUI.SetActive(false);
    }

    private void Update()
    {
        if (canShop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                movement.stun = true;
                cinemachineBrain.enabled = false;
                shopUI.SetActive(true);
            }
        }
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canShop = true;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canShop = false;
            canvas.SetActive(false);
        }
    }
}
