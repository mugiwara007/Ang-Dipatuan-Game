using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint1 : MonoBehaviour
{
    Quest4Script quest4Script;
    WaypointScript waypointScript;
    public GameObject box;

    private void Awake()
    {
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        quest4Script = GameObject.FindGameObjectWithTag("Quest4Collider").GetComponent<Quest4Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointScript.enabled = false;
            box.SetActive(false);
            quest4Script.quest.goal.Waypoint();
        }
    }
}
