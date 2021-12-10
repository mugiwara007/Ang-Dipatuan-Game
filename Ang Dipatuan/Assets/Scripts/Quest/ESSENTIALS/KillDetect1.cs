using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDetect1 : MonoBehaviour
{
    public GameObject kill1;

    public GameObject EnemyRifleMan;

    QuestGiver1 questGiver;

    private void Awake()
    {
        questGiver = GameObject.FindGameObjectWithTag("Q1").GetComponent<QuestGiver1>();
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
            kill1.SetActive(false);
            questGiver.quest.goal.EnemyKilled();
        }


    }

    public bool IsEnemiesinCampKilled()
    {
        return gameObject.transform.GetChild(0).childCount == 0;
    }
}
