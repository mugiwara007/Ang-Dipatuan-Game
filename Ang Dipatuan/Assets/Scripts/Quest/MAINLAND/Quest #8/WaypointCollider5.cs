using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollider5 : MonoBehaviour
{
    public Quest8EnemySpawner quest8EnemySpawner;
    public GameObject box;
    public GameObject wall;

    GameObject waypointMarker;

    private void Start()
    {
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            waypointMarker.SetActive(false);
            box.SetActive(false);
            wall.SetActive(true);
            quest8EnemySpawner.wallActive5 = true;
        }
    }
}
