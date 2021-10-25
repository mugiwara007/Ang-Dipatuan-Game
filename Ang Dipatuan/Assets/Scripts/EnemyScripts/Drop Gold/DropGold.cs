using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGold : MonoBehaviour
{

    EnemyStats myStats;

    public GameObject goldGameObject;


    private void Awake()
    {
        myStats = gameObject.GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myStats.ehealth <= 0)
        {
            Instantiate(goldGameObject, transform.position, Quaternion.identity);
            gameObject.GetComponent<DropGold>().enabled = false;
        }
    }
}
