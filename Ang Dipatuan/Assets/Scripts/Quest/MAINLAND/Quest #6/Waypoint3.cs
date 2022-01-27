using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint3 : MonoBehaviour
{
    Quest6Script quest6Script;
    GameObject waypoint;
    public GameObject box;


    private void Awake()
    {
        waypoint = GameObject.FindGameObjectWithTag("Waypont");
        quest6Script = GameObject.FindGameObjectWithTag("Quest5Collider").GetComponent<Quest6Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypoint.SetActive(false);
            box.SetActive(false);
        }
    }
}
