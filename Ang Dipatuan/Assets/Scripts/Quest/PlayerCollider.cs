using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    Activator activator;
    WaypointScript waypointScript;
    public GameObject box;

    private void Awake()
    {
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        activator = GameObject.FindGameObjectWithTag("Activator").GetComponent<Activator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointScript.enabled = false;
            box.SetActive(false);
            activator.quest.goal.Waypoint();
        }
    }
}
