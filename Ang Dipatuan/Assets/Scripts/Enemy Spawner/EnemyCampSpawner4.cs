using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCampSpawner4 : MonoBehaviour
{
    public GameObject EnemySoldier1;
    public GameObject EnemySoldier3;
    public GameObject SpanishSoldier;
    public GameObject EnemyRifleMan;
    SaveQuestScript2 saveQuestScript2;
    Quest7Script quest7Script;

    private float timer;

    private void Awake()
    {
        quest7Script = GameObject.FindGameObjectWithTag("Quest7Collider").GetComponent<Quest7Script>();
        saveQuestScript2 = GameObject.FindGameObjectWithTag("Updater2").GetComponent<SaveQuestScript2>();
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

        var enemySoldier6 = Instantiate(EnemyRifleMan, gameObject.transform.GetChild(5).position, gameObject.transform.GetChild(5).rotation);
        enemySoldier6.transform.parent = gameObject.transform.GetChild(5);
    }


    // Update is called once per frame
    void Update()
    {
        if (IsEnemiesinCampKilled())
        {
            timer += Time.deltaTime;

            if (saveQuestScript2.CurrQuest2 == 6)
            {
                quest7Script.quest.goal.EnemyKilled();
            }

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
            && gameObject.transform.GetChild(4).childCount == 0
            && gameObject.transform.GetChild(5).childCount == 0;
    }
}
