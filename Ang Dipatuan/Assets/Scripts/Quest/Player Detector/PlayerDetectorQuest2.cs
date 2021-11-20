using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorQuest2 : MonoBehaviour
{
    GameObject canvas;

    QuestGiver1 questGiver;

    bool canAcceptQuest;

    private void Awake()
    {
        canvas = gameObject.transform.parent.Find("Canvas").gameObject;
        questGiver = GameObject.FindGameObjectWithTag("Q2").GetComponent<QuestGiver1>();
        canAcceptQuest = false;
    }

    private void Update()
    {
        if (canAcceptQuest == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                questGiver.Quest1();
            }
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(true);
            canAcceptQuest = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canvas.SetActive(false);
            canAcceptQuest = false;
        }
    }
}
