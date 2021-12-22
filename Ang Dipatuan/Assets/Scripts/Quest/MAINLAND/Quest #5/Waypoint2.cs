using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint2 : MonoBehaviour
{
    Quest5Script quest5Script;
    WaypointScript waypointScript;
    public GameObject box;

    private void Awake()
    {
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        quest5Script = GameObject.FindGameObjectWithTag("Quest5Collider").GetComponent<Quest5Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointScript.enabled = false;
            box.SetActive(false);
            quest5Script.quest.goal.Waypoint();
        }
    }
}
