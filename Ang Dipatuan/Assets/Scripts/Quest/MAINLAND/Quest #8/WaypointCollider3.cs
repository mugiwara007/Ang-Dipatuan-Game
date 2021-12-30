using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollider3 : MonoBehaviour
{
    public Quest8EnemySpawner quest8EnemySpawner;
    public GameObject box;
    public GameObject wall1;
    public GameObject wall2;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            box.SetActive(false);
            wall1.SetActive(true);
            wall2.SetActive(true);
            quest8EnemySpawner.wallActive3 = true;
        }
    }
}
