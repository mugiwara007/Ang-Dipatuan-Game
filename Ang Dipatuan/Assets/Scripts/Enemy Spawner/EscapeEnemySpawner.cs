using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeEnemySpawner : MonoBehaviour
{
    public GameObject EnemySoldier1;
    public GameObject EnemySoldier3;
    public GameObject SpanishSoldier;
    public GameObject EnemyRifleMan;

    private float timer;

    private void Awake()
    {
        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        var enemySoldier1 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);
        enemySoldier1.transform.parent = gameObject.transform.GetChild(0);

        var enemySoldier2 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).rotation);
        enemySoldier2.transform.parent = gameObject.transform.GetChild(1);

        var enemySoldier3 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(2).position, gameObject.transform.GetChild(2).rotation);
        enemySoldier3.transform.parent = gameObject.transform.GetChild(2);

        var enemySoldier4 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(3).position, gameObject.transform.GetChild(3).rotation);
        enemySoldier4.transform.parent = gameObject.transform.GetChild(3);

        var enemySoldier5 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(4).position, gameObject.transform.GetChild(4).rotation);
        enemySoldier5.transform.parent = gameObject.transform.GetChild(4);

        var enemySoldier6 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(5).position, gameObject.transform.GetChild(5).rotation);
        enemySoldier6.transform.parent = gameObject.transform.GetChild(5);

        var enemySoldier7 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(6).position, gameObject.transform.GetChild(6).rotation);
        enemySoldier7.transform.parent = gameObject.transform.GetChild(6);

        var enemySoldier8 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(7).position, gameObject.transform.GetChild(7).rotation);
        enemySoldier8.transform.parent = gameObject.transform.GetChild(7);
    }
}
