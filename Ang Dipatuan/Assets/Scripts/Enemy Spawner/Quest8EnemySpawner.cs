using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest8EnemySpawner : MonoBehaviour
{
    public GameObject EnemySoldier1;
    public GameObject EnemySoldier3;
    public GameObject SpanishSoldier;
    public GameObject EnemyRifleMan;
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;
    Quest8Script quest8Script;

    GameObject waypointMarker;

    public GameObject wall4;
    public GameObject wall5;
    public GameObject SecondStageWallStart;
    public GameObject SecondStageWallMid;
    public GameObject SecondStageWallEnd;
    public GameObject ThirdStageWall;

    private bool ctr1 = true, ctr2 = true, ctr3 = true;

    private bool isThirdWave = false;

    private bool canSpawn1 = true, canSpawn2 = true, canSpawn3 = true, canSpawn4 = true, canSpawn5 = true;

    public bool wallActive1 = false, wallActive2 = false, wallActive3 = false;

    private void Awake()
    {
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
        quest8Script = GameObject.FindGameObjectWithTag("Quest8Collider").GetComponent<Quest8Script>();

        wall4.SetActive(true);
        wall5.SetActive(false);
        SecondStageWallStart.SetActive(false);
        SecondStageWallMid.SetActive(false);
        SecondStageWallEnd.SetActive(false);
        ThirdStageWall.SetActive(false);

        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        var enemySoldier1 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);
        enemySoldier1.transform.parent = gameObject.transform.GetChild(0);

        var enemySoldier2 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(1).position, gameObject.transform.GetChild(1).rotation);
        enemySoldier2.transform.parent = gameObject.transform.GetChild(1);

        var enemySoldier3 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(2).position, gameObject.transform.GetChild(2).rotation);
        enemySoldier3.transform.parent = gameObject.transform.GetChild(2);
    }

    private void Update()
    {
        if (canSpawn1 == true)
        {
            if (IsFirstWaveKilled() == true && ctr1 == true)
            {
                wall4.SetActive(false);
                ctr1 = false;
            }

            if (IsFirstWaveKilled() == true && wallActive1 == true)
            {
                wall4.SetActive(true);

                var enemySoldier4 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(3).position, gameObject.transform.GetChild(3).rotation);
                enemySoldier4.transform.parent = gameObject.transform.GetChild(3);

                var enemySoldier5 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(4).position, gameObject.transform.GetChild(4).rotation);
                enemySoldier5.transform.parent = gameObject.transform.GetChild(4);

                var enemySoldier6 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(5).position, gameObject.transform.GetChild(5).rotation);
                enemySoldier6.transform.parent = gameObject.transform.GetChild(5);

                var enemySoldier7 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(6).position, gameObject.transform.GetChild(6).rotation);
                enemySoldier7.transform.parent = gameObject.transform.GetChild(6);

                var enemySoldier8 = Instantiate(Boss1, gameObject.transform.GetChild(7).position, gameObject.transform.GetChild(7).rotation);
                enemySoldier8.transform.parent = gameObject.transform.GetChild(7);

                canSpawn1 = false;
            }
        }

        if (canSpawn2 == true)
        {
            if (IsFirstWaveKilled() == true && ctr2 == true)
            {
                SecondStageWallStart.SetActive(false);
                ctr2 = false;
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true)
            {
                SecondStageWallStart.SetActive(true);

                var enemySoldier9 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(8).position, gameObject.transform.GetChild(8).rotation);
                enemySoldier9.transform.parent = gameObject.transform.GetChild(8);

                var enemySoldier10 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(9).position, gameObject.transform.GetChild(9).rotation);
                enemySoldier10.transform.parent = gameObject.transform.GetChild(9);

                var enemySoldier11 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(10).position, gameObject.transform.GetChild(10).rotation);
                enemySoldier11.transform.parent = gameObject.transform.GetChild(10);

                canSpawn2 = false;
            }
        }

        if (canSpawn3 == true)
        {
            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && wallActive2 == true)
            {
                var enemySoldier12 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(11).position, gameObject.transform.GetChild(11).rotation);
                enemySoldier12.transform.parent = gameObject.transform.GetChild(11);

                var enemySoldier13 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(12).position, gameObject.transform.GetChild(12).rotation);
                enemySoldier13.transform.parent = gameObject.transform.GetChild(12);

                var enemySoldier14 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(13).position, gameObject.transform.GetChild(13).rotation);
                enemySoldier14.transform.parent = gameObject.transform.GetChild(13);

                var enemySoldier15 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(14).position, gameObject.transform.GetChild(14).rotation);
                enemySoldier15.transform.parent = gameObject.transform.GetChild(14);

                var enemySoldier16 = Instantiate(Boss2, gameObject.transform.GetChild(15).position, gameObject.transform.GetChild(15).rotation);
                enemySoldier16.transform.parent = gameObject.transform.GetChild(15);

                canSpawn3 = false;
            }
        }

        if (canSpawn4 == true)
        {
            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true)
            {
                var enemySoldier17 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(16).position, gameObject.transform.GetChild(16).rotation);
                enemySoldier17.transform.parent = gameObject.transform.GetChild(16);

                var enemySoldier18 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(17).position, gameObject.transform.GetChild(17).rotation);
                enemySoldier18.transform.parent = gameObject.transform.GetChild(17);

                var enemySoldier19 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(18).position, gameObject.transform.GetChild(18).rotation);
                enemySoldier19.transform.parent = gameObject.transform.GetChild(18);

                canSpawn4 = false;
            }
        }

        if (canSpawn5 == true)
        {
            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true && IsThirdWaveKilled() == true && wallActive3 == true)
            {
                var enemySoldier20 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(19).position, gameObject.transform.GetChild(19).rotation);
                enemySoldier20.transform.parent = gameObject.transform.GetChild(19);

                var enemySoldier21 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(20).position, gameObject.transform.GetChild(20).rotation);
                enemySoldier21.transform.parent = gameObject.transform.GetChild(20);

                var enemySoldier22 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(21).position, gameObject.transform.GetChild(21).rotation);
                enemySoldier22.transform.parent = gameObject.transform.GetChild(21);

                var enemySoldier23 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(22).position, gameObject.transform.GetChild(22).rotation);
                enemySoldier23.transform.parent = gameObject.transform.GetChild(22);

                var enemySoldier24 = Instantiate(Boss3, gameObject.transform.GetChild(23).position, gameObject.transform.GetChild(23).rotation);
                enemySoldier24.transform.parent = gameObject.transform.GetChild(23);

                isThirdWave = true;

                canSpawn5 = false;
            }
        }

        if (IsThirdBossKilled() == true && isThirdWave == true)
        {
            quest8Script.quest.goal.EnemyKilled();
        }
    }

    public bool IsFirstWaveKilled()
    {
        return gameObject.transform.GetChild(0).childCount == 0
            && gameObject.transform.GetChild(1).childCount == 0
            && gameObject.transform.GetChild(2).childCount == 0;
    }

    public bool IsFirstBossKilled()
    {
        return gameObject.transform.GetChild(3).childCount == 0
            && gameObject.transform.GetChild(4).childCount == 0
            && gameObject.transform.GetChild(5).childCount == 0
            && gameObject.transform.GetChild(6).childCount == 0
            && gameObject.transform.GetChild(7).childCount == 0;
    }

    public bool IsSecondWaveKilled()
    {
        return gameObject.transform.GetChild(8).childCount == 0
            && gameObject.transform.GetChild(9).childCount == 0
            && gameObject.transform.GetChild(10).childCount == 0;
    }

    public bool IsSecondBossKilled()
    {
        return gameObject.transform.GetChild(11).childCount == 0
            && gameObject.transform.GetChild(12).childCount == 0
            && gameObject.transform.GetChild(13).childCount == 0
            && gameObject.transform.GetChild(14).childCount == 0
            && gameObject.transform.GetChild(15).childCount == 0;
    }
    public bool IsThirdWaveKilled()
    {
        return gameObject.transform.GetChild(16).childCount == 0
            && gameObject.transform.GetChild(17).childCount == 0
            && gameObject.transform.GetChild(18).childCount == 0;
    }

    public bool IsThirdBossKilled()
    {
        return gameObject.transform.GetChild(19).childCount == 0
            && gameObject.transform.GetChild(20).childCount == 0
            && gameObject.transform.GetChild(21).childCount == 0
            && gameObject.transform.GetChild(22).childCount == 0
            && gameObject.transform.GetChild(23).childCount == 0;
    }
}
