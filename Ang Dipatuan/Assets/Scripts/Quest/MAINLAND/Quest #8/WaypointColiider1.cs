using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointColiider1 : MonoBehaviour
{
    public Quest8EnemySpawner quest8EnemySpawner;
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
            box.SetActive(false);
            quest8EnemySpawner.wallActive1 = true;
        }
    }
}
