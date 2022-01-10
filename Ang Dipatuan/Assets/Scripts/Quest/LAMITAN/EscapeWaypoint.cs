using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeWaypoint : MonoBehaviour
{
    EscapeQuest escapeQuest;
    GameObject waypointMarker;
    public GameObject box;

    private void Awake()
    {
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
        escapeQuest = GameObject.FindGameObjectWithTag("EscapeCollider").GetComponent<EscapeQuest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointMarker.SetActive(false);
            box.SetActive(false);
            escapeQuest.quest.goal.Waypoint();
        }
    }
}
