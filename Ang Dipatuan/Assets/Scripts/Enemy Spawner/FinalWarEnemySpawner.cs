using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalWarEnemySpawner : MonoBehaviour
{
    public GameObject EnemySoldier1;
    public GameObject EnemySoldier3;
    public GameObject SpanishSoldier;
    public GameObject EnemyRifleMan;
    public GameObject Boss;

    private bool isThirdWave = false;

    Quest9Script quest9Script;

    private bool canSpawn1 = true, canSpawn2 = true;

    private void Awake()
    {
        quest9Script = GameObject.FindGameObjectWithTag("Quest9Collider").GetComponent<Quest9Script>();
        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        var enemySoldier1 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);
        enemySoldier1.transform.parent = gameObject.transform.GetChild(0);

        var enemySoldier2 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).rotation);
        enemySoldier2.transform.parent = gameObject.transform.GetChild(1);

        var enemySoldier3 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(2).position, gameObject.transform.GetChild(2).rotation);
        enemySoldier3.transform.parent = gameObject.transform.GetChild(2);

        var enemySoldier4 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(3).position, gameObject.transform.GetChild(3).rotation);
        enemySoldier4.transform.parent = gameObject.transform.GetChild(3);
    }


    // Update is called once per frame
    void Update()
    {
        if (canSpawn1 == true)
        {
            if (IsFirstWaveKilled() == true)
            {
                var enemySoldier5 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(4).position, gameObject.transform.GetChild(4).rotation);
                enemySoldier5.transform.parent = gameObject.transform.GetChild(4);

                var enemySoldier6 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(5).position, gameObject.transform.GetChild(5).rotation);
                enemySoldier6.transform.parent = gameObject.transform.GetChild(5);

                var enemySoldier7 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(6).position, gameObject.transform.GetChild(6).rotation);
                enemySoldier7.transform.parent = gameObject.transform.GetChild(6);

                var enemySoldier8 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(7).position, gameObject.transform.GetChild(7).rotation);
                enemySoldier8.transform.parent = gameObject.transform.GetChild(7);

                var enemySoldier9 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(8).position, gameObject.transform.GetChild(8).rotation);
                enemySoldier9.transform.parent = gameObject.transform.GetChild(8);

                canSpawn1 = false;
            }
        }

        if (canSpawn2 == true)
        {
            if (IsFirstWaveKilled() == true && IsSecondWaveKilled() == true)
            {
                var enemySoldier10 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(9).position, gameObject.transform.GetChild(9).rotation);
                enemySoldier10.transform.parent = gameObject.transform.GetChild(9);

                var enemySoldier11 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(10).position, gameObject.transform.GetChild(10).rotation);
                enemySoldier11.transform.parent = gameObject.transform.GetChild(10);

                var enemySoldier12 = Instantiate(Boss, gameObject.transform.GetChild(11).position, gameObject.transform.GetChild(11).rotation);
                enemySoldier12.transform.parent = gameObject.transform.GetChild(11);
                
                isThirdWave = true;

                canSpawn2 = false;
            }
        }

        if (IsComplete() == true && isThirdWave == true)
        {
            quest9Script.quest.goal.EnemyKilled();
        }
    }

    public bool IsFirstWaveKilled()
    {

        return gameObject.transform.GetChild(0).childCount == 0
            && gameObject.transform.GetChild(1).childCount == 0
            && gameObject.transform.GetChild(2).childCount == 0
            && gameObject.transform.GetChild(3).childCount == 0;
    }

    public bool IsSecondWaveKilled()
    {
        return gameObject.transform.GetChild(4).childCount == 0
            && gameObject.transform.GetChild(5).childCount == 0
            && gameObject.transform.GetChild(6).childCount == 0
            && gameObject.transform.GetChild(7).childCount == 0
            && gameObject.transform.GetChild(8).childCount == 0;
    }
    public bool IsComplete()
    {
        return gameObject.transform.GetChild(9).childCount == 0
            && gameObject.transform.GetChild(10).childCount == 0
            && gameObject.transform.GetChild(11).childCount == 0;
    }
}
