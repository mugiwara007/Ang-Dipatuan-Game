using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCampSpawner6 : MonoBehaviour
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
        var enemySoldier1 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);
        enemySoldier1.transform.parent = gameObject.transform.GetChild(0);

        var enemySoldier2 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).rotation);
        enemySoldier2.transform.parent = gameObject.transform.GetChild(1);

        var enemySoldier3 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(2).position, gameObject.transform.GetChild(2).rotation);
        enemySoldier3.transform.parent = gameObject.transform.GetChild(2);

        var enemySoldier4 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(3).position, gameObject.transform.GetChild(3).rotation);
        enemySoldier4.transform.parent = gameObject.transform.GetChild(3);

        var enemySoldier5 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(4).position, gameObject.transform.GetChild(4).rotation);
        enemySoldier5.transform.parent = gameObject.transform.GetChild(4);
    }


    // Update is called once per frame
    void Update()
    {
        if (IsEnemiesinCampKilled())
        {
            timer += Time.deltaTime;

            //After 150 Seconds Enemies in This Camp will be Respawned
            if (timer >= 200f)
            {
                InstantiateEnemies();
                timer = 0f;
            }
        }


    }

    public bool IsEnemiesinCampKilled()
    {
        return gameObject.transform.GetChild(0).childCount == 0
            && gameObject.transform.GetChild(1).childCount == 0
            && gameObject.transform.GetChild(2).childCount == 0
            && gameObject.transform.GetChild(3).childCount == 0
            && gameObject.transform.GetChild(4).childCount == 0;
    }
}
