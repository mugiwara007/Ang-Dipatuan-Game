using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointCollider2 : MonoBehaviour
{
    public Quest8EnemySpawner quest8EnemySpawner;
    public GameObject box;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            box.SetActive(false);
            quest8EnemySpawner.wallActive2 = true;
        }
    }
}
