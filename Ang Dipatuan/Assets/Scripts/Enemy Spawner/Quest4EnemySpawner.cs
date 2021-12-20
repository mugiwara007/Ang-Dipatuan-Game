using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest4EnemySpawner : MonoBehaviour
{
    public GameObject EnemySoldier1;
    public GameObject EnemySoldier3;
    public GameObject SpanishSoldier;
    public GameObject EnemyRifleMan;

    private void Awake()
    {
        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        var enemySoldier1 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);
        enemySoldier1.transform.parent = gameObject.transform.GetChild(0);

        var enemySoldier2 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).rotation);
        enemySoldier2.transform.parent = gameObject.transform.GetChild(1);

        var enemySoldier3 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(2).position, gameObject.transform.GetChild(2).rotation);
        enemySoldier3.transform.parent = gameObject.transform.GetChild(2);

        var enemySoldier4 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(3).position, gameObject.transform.GetChild(3).rotation);
        enemySoldier4.transform.parent = gameObject.transform.GetChild(3);

        var enemySoldier5 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(4).position, gameObject.transform.GetChild(4).rotation);
        enemySoldier5.transform.parent = gameObject.transform.GetChild(4);

        var enemySoldier6 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(5).position, gameObject.transform.GetChild(5).rotation);
        enemySoldier6.transform.parent = gameObject.transform.GetChild(5);

        var enemySoldier7 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(6).position, gameObject.transform.GetChild(6).rotation);
        enemySoldier7.transform.parent = gameObject.transform.GetChild(6);

        var enemySoldier8 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(7).position, gameObject.transform.GetChild(7).rotation);
        enemySoldier8.transform.parent = gameObject.transform.GetChild(7);

        var enemySoldier9 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(8).position, gameObject.transform.GetChild(8).rotation);
        enemySoldier9.transform.parent = gameObject.transform.GetChild(8);

        var enemySoldier10 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(9).position, gameObject.transform.GetChild(9).rotation);
        enemySoldier10.transform.parent = gameObject.transform.GetChild(9);

        var enemySoldier11 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(10).position, gameObject.transform.GetChild(10).rotation);
        enemySoldier11.transform.parent = gameObject.transform.GetChild(10);

        var enemySoldier12 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(11).position, gameObject.transform.GetChild(11).rotation);
        enemySoldier12.transform.parent = gameObject.transform.GetChild(11);

        var enemySoldier13 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(12).position, gameObject.transform.GetChild(12).rotation);
        enemySoldier13.transform.parent = gameObject.transform.GetChild(12);
    }
}
