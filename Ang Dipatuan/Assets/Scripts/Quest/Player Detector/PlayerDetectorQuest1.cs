using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectorQuest1 : MonoBehaviour
{
    GameObject canvas;

    QuestGiver1 questGiver;

    WaypointScript waypointScript;

    GameObject npc1;

    bool canAcceptQuest;

    private void Awake()
    {
        canvas = gameObject.transform.parent.Find("Canvas").gameObject;
        questGiver = GameObject.FindGameObjectWithTag("Q1").GetComponent<QuestGiver1>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        npc1 = GameObject.FindGameObjectWithTag("Q1").gameObject;
        canAcceptQuest = false;
    }

    private void Update()
    {
        if (canAcceptQuest == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                waypointScript.target = npc1.transform;
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
