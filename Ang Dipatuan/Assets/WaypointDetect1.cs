using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaypointDetect1 : MonoBehaviour
{
    public GameObject waypoint;

    QuestGiver1 questGiver;

    private void Awake()
    {
        questGiver = GameObject.FindGameObjectWithTag("Q1").GetComponent<QuestGiver1>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            waypoint.SetActive(false);
            questGiver.quest.goal.Waypoint();
        }
    }
}
