using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarSpawnerScript1 : MonoBehaviour
{
    public GameObject EnemySoldier1;
    public GameObject EnemySoldier3;
    public GameObject SpanishSoldier;
    public GameObject EnemyRifleMan;

    private bool isThirdWave = false;

    WarScript warScript;

    private bool canSpawn1 = true, canSpawn2 = true;

    private void Awake()
    {
        warScript = GameObject.FindGameObjectWithTag("TELEPORT").GetComponent<WarScript>();
        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        var enemySoldier1 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);
        enemySoldier1.transform.parent = gameObject.transform.GetChild(0);
    }


    // Update is called once per frame
    void Update()
    {
        if (canSpawn1 == true)
        {
            if (IsFirstWaveKilled() == true)
            {
                var enemySoldier2 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).rotation);
                enemySoldier2.transform.parent = gameObject.transform.GetChild(1);

                var enemySoldier3 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(2).position, gameObject.transform.GetChild(2).rotation);
                enemySoldier3.transform.parent = gameObject.transform.GetChild(2);
                canSpawn1 = false;
            }
        }
        
        if (canSpawn2 == true)
        {
            if (IsFirstWaveKilled() == true && IsSecondWaveKilled() == true)
            {
                var enemySoldier4 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(3).position, gameObject.transform.GetChild(3).rotation);
                enemySoldier4.transform.parent = gameObject.transform.GetChild(3);

                var enemySoldier5 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(4).position, gameObject.transform.GetChild(4).rotation);
                enemySoldier5.transform.parent = gameObject.transform.GetChild(4);

                var enemySoldier6 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(5).position, gameObject.transform.GetChild(5).rotation);
                enemySoldier6.transform.parent = gameObject.transform.GetChild(5);

                isThirdWave = true;

                canSpawn2 = false;
            }
        }

        if (IsComplete() == true && isThirdWave == true)
        {
            warScript.quest.goal.EnemyKilled();
        }
    }

    public bool IsFirstWaveKilled()
    {

        return gameObject.transform.GetChild(0).childCount == 0;
    }

    public bool IsSecondWaveKilled()
    {
        return gameObject.transform.GetChild(1).childCount == 0
            && gameObject.transform.GetChild(2).childCount == 0;
    }
    public bool IsComplete()
    {
        return gameObject.transform.GetChild(3).childCount == 0
            && gameObject.transform.GetChild(4).childCount == 0
            && gameObject.transform.GetChild(5).childCount == 0;
    }

}
