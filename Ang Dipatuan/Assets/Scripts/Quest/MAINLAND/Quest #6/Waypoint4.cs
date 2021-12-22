using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint4 : MonoBehaviour
{
    Quest6Script quest6Script;
    WaypointScript waypointScript;
    public GameObject box;

    private void Awake()
    {
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        quest6Script = GameObject.FindGameObjectWithTag("Quest6NPC").GetComponent<Quest6Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointScript.enabled = false;
            box.SetActive(false);
            quest6Script.quest.goal.Waypoint();
        }
    }
}
