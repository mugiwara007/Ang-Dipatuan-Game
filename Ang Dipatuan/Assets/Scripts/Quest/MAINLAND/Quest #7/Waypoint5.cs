using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint5 : MonoBehaviour
{
    GameObject waypointMarker;
    public GameObject box;

    private void Awake()
    {
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointMarker.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointMarker.SetActive(true);
        }
    }
}
