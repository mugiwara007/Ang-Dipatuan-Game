using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint2 : MonoBehaviour
{
    Quest5Script quest5Script;
    public GameObject box;

    private void Awake()
    {
        quest5Script = GameObject.FindGameObjectWithTag("Quest5Collider").GetComponent<Quest5Script>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            box.SetActive(false);
            quest5Script.quest.goal.Waypoint();
        }
    }
}
