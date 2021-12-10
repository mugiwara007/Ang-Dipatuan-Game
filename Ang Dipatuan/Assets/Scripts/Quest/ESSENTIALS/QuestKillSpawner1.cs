using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestKillSpawner1 : MonoBehaviour
{
    public GameObject EnemyRifleMan;

    private float timer;

    private void Awake()
    {
        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        var enemySoldier9 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(0).position, gameObject.transform.GetChild(0).rotation);
        enemySoldier9.transform.parent = gameObject.transform.GetChild(0);
    }


    // Update is called once per frame
    void Update()
    {
        if (IsEnemiesinCampKilled())
        {
            timer += Time.deltaTime;

            //After 150 Seconds Enemies in This Camp will be Respawned
            if (timer >= 150f)
            {
                InstantiateEnemies();
                timer = 0f;
            }
        }


    }

    public bool IsEnemiesinCampKilled()
    {
        return gameObject.transform.GetChild(0).childCount == 0;
    }
}
