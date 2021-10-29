using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorQuest1 : MonoBehaviour
{
    GameObject canvas;
    private bool canShop;

    QuestGiver1 questGiver;

    private void Awake()
    {
        canvas = gameObject.transform.parent.Find("Canvas").gameObject;
        questGiver = GameObject.FindGameObjectWithTag("Q1").GetComponent<QuestGiver1>();
    }

    private void Update()
    {

            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                questGiver.Quest1();
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
