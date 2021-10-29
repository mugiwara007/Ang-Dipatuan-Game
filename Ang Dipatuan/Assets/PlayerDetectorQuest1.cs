using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorQuest1 : MonoBehaviour
{
    GameObject canvas;
    private bool canShop;

    GameObject questUI;

    private void Awake()
    {
        canvas = gameObject.transform.parent.Find("Canvas").gameObject;

        questUI = GameObject.FindGameObjectWithTag("QuestUI");
    }

    private void Update()
    {
        if (canShop)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;

            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(false);
        }
    }
}
