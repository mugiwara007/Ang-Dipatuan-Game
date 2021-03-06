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
    public GameObject basket;
    public GameObject coconut;
    public GameObject avocado;
    public GameObject pineapple;
    Quest8Script quest8Script;

    GameObject waypointMarker;
    WaypointScript waypointScript;

    public GameObject wall4;
    public GameObject wall5;
    public GameObject SecondStageWallStart;
    public GameObject SecondStageWallMid;
    public GameObject SecondStageWallEnd;
    public GameObject ThirdStageWall;

    private bool ctr1 = true, ctr2 = true, ctr3 = true, ctr4 = true, ctr5 = true;

    private bool isThirdWave = false;

    private bool canSpawn1 = true, canSpawn2 = true, canSpawn3 = true, canSpawn4 = true, canSpawn5 = true;

    private int counter = 0;

    public bool wallActive1 = false, wallActive2 = false, wallActive3 = false, wallActive4 = false, wallActive5 = false;

    private void Awake()
    {
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
        quest8Script = GameObject.FindGameObjectWithTag("Quest8Collider").GetComponent<Quest8Script>();

        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();

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
                SecondStageWallStart.SetActive(true);
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

                counter = 1;

                canSpawn1 = false;
                wallActive1 = false;
            }
        }


        if (canSpawn2 == true)
        {
            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true &&  ctr2 == true && counter == 1)
            {

                var fruit1 = Instantiate(coconut, gameObject.transform.GetChild(24).position, gameObject.transform.GetChild(24).rotation);
                fruit1.transform.parent = gameObject.transform.GetChild(24);

                var fruit2 = Instantiate(coconut, gameObject.transform.GetChild(25).position, gameObject.transform.GetChild(25).rotation);
                fruit2.transform.parent = gameObject.transform.GetChild(25);

                var fruit3 = Instantiate(pineapple, gameObject.transform.GetChild(26).position, gameObject.transform.GetChild(26).rotation);
                fruit3.transform.parent = gameObject.transform.GetChild(26);

                var fruit4 = Instantiate(avocado, gameObject.transform.GetChild(27).position, gameObject.transform.GetChild(27).rotation);
                fruit4.transform.parent = gameObject.transform.GetChild(27);

                var fruit5 = Instantiate(basket, gameObject.transform.GetChild(28).position, gameObject.transform.GetChild(28).rotation);
                fruit5.transform.parent = gameObject.transform.GetChild(28);

                ctr2 = false;
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && Fruit1() == true)
            {
                SecondStageWallStart.SetActive(false);
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && wallActive2 == true)
            {

                var enemySoldier9 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(8).position, gameObject.transform.GetChild(8).rotation);
                enemySoldier9.transform.parent = gameObject.transform.GetChild(8);

                var enemySoldier10 = Instantiate(EnemySoldier1, gameObject.transform.GetChild(9).position, gameObject.transform.GetChild(9).rotation);
                enemySoldier10.transform.parent = gameObject.transform.GetChild(9);

                var enemySoldier11 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(10).position, gameObject.transform.GetChild(10).rotation);
                enemySoldier11.transform.parent = gameObject.transform.GetChild(10);

                counter = 2;

                canSpawn2 = false;
                wallActive2 = false;
            }
        }

        if (canSpawn3 == true)
        {
            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true)
            {
                SecondStageWallMid.SetActive(false);
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && wallActive3 == true)
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

                counter = 3;

                canSpawn3 = false;
                wallActive3 = false;
            }
        }

        if (canSpawn4 == true)
        {
            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true && ctr4 == true && counter == 3)
            {

                var fruit6 = Instantiate(coconut, gameObject.transform.GetChild(29).position, gameObject.transform.GetChild(29).rotation);
                fruit6.transform.parent = gameObject.transform.GetChild(29);

                var fruit7 = Instantiate(coconut, gameObject.transform.GetChild(30).position, gameObject.transform.GetChild(30).rotation);
                fruit7.transform.parent = gameObject.transform.GetChild(30);

                var fruit8 = Instantiate(pineapple, gameObject.transform.GetChild(31).position, gameObject.transform.GetChild(31).rotation);
                fruit8.transform.parent = gameObject.transform.GetChild(31);

                var fruit9 = Instantiate(avocado, gameObject.transform.GetChild(32).position, gameObject.transform.GetChild(32).rotation);
                fruit9.transform.parent = gameObject.transform.GetChild(32);

                var fruit10 = Instantiate(basket, gameObject.transform.GetChild(33).position, gameObject.transform.GetChild(33).rotation);
                fruit10.transform.parent = gameObject.transform.GetChild(33);

                counter = 4;
                ctr4 = false;
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true && Fruit2() == true)
            {
                SecondStageWallEnd.SetActive(false);
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true && wallActive4 == true)
            {
                var enemySoldier17 = Instantiate(EnemySoldier3, gameObject.transform.GetChild(16).position, gameObject.transform.GetChild(16).rotation);
                enemySoldier17.transform.parent = gameObject.transform.GetChild(16);

                var enemySoldier18 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(17).position, gameObject.transform.GetChild(17).rotation);
                enemySoldier18.transform.parent = gameObject.transform.GetChild(17);

                var enemySoldier19 = Instantiate(SpanishSoldier, gameObject.transform.GetChild(18).position, gameObject.transform.GetChild(18).rotation);
                enemySoldier19.transform.parent = gameObject.transform.GetChild(18);

                counter = 5;

                canSpawn4 = false;
                wallActive4 = false;
            }
        }

        if (canSpawn5 == true)
        {
            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true && IsThirdWaveKilled() == true && ctr5 == true && counter == 5)
            {

                var fruit11 = Instantiate(coconut, gameObject.transform.GetChild(34).position, gameObject.transform.GetChild(34).rotation);
                fruit11.transform.parent = gameObject.transform.GetChild(34);

                var fruit12 = Instantiate(coconut, gameObject.transform.GetChild(35).position, gameObject.transform.GetChild(35).rotation);
                fruit12.transform.parent = gameObject.transform.GetChild(35);

                var fruit13 = Instantiate(pineapple, gameObject.transform.GetChild(36).position, gameObject.transform.GetChild(36).rotation);
                fruit13.transform.parent = gameObject.transform.GetChild(36);

                var fruit14 = Instantiate(avocado, gameObject.transform.GetChild(37).position, gameObject.transform.GetChild(37).rotation);
                fruit14.transform.parent = gameObject.transform.GetChild(37);

                var fruit15 = Instantiate(basket, gameObject.transform.GetChild(38).position, gameObject.transform.GetChild(38).rotation);
                fruit15.transform.parent = gameObject.transform.GetChild(38);

                counter = 6;
                ctr5 = false;
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true && IsThirdWaveKilled() == true && Fruit2() == true)
            {
                ThirdStageWall.SetActive(false);
            }

            if (IsFirstWaveKilled() == true && IsFirstBossKilled() == true && IsSecondWaveKilled() == true && IsSecondBossKilled() == true && IsThirdWaveKilled() == true && wallActive5 == true)
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
                wallActive5 = false;
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
        ctr3 = true;
        return gameObject.transform.GetChild(8).childCount == 0
            && gameObject.transform.GetChild(9).childCount == 0
            && gameObject.transform.GetChild(10).childCount == 0
            && ctr3;
    }

    public bool IsSecondBossKilled()
    {
        ctr4 = true;
        return gameObject.transform.GetChild(11).childCount == 0
            && gameObject.transform.GetChild(12).childCount == 0
            && gameObject.transform.GetChild(13).childCount == 0
            && gameObject.transform.GetChild(14).childCount == 0
            && gameObject.transform.GetChild(15).childCount == 0
            && ctr4;
    }
    public bool IsThirdWaveKilled()
    {
        ctr5 = true;
        return gameObject.transform.GetChild(16).childCount == 0
            && gameObject.transform.GetChild(17).childCount == 0
            && gameObject.transform.GetChild(18).childCount == 0
            && ctr5;
    }

    public bool IsThirdBossKilled()
    {
        return gameObject.transform.GetChild(19).childCount == 0
            && gameObject.transform.GetChild(20).childCount == 0
            && gameObject.transform.GetChild(21).childCount == 0
            && gameObject.transform.GetChild(22).childCount == 0
            && gameObject.transform.GetChild(23).childCount == 0;
    }

    public bool Fruit1()
    {
        return gameObject.transform.GetChild(24).childCount == 0
            && gameObject.transform.GetChild(25).childCount == 0
            && gameObject.transform.GetChild(26).childCount == 0
            && gameObject.transform.GetChild(27).childCount == 0
            && gameObject.transform.GetChild(28).childCount == 0;
    }

    public bool Fruit2()
    {
        return gameObject.transform.GetChild(29).childCount == 0
            && gameObject.transform.GetChild(30).childCount == 0
            && gameObject.transform.GetChild(31).childCount == 0
            && gameObject.transform.GetChild(32).childCount == 0
            && gameObject.transform.GetChild(33).childCount == 0;
    }
}
