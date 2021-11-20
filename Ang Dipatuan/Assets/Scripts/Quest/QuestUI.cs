using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class QuestUI
{
    public bool isActive;

    public string title;
    public string desc;
    public int goldReward;
    public int currentQuest = 1;

    public QuestGoal goal;

    GameObject playerDetectorQuest1;
    GameObject playerDetectorQuest2;

    void Awake()
    {
        playerDetectorQuest1 = GameObject.FindGameObjectWithTag("P1").gameObject;
        playerDetectorQuest2 = GameObject.FindGameObjectWithTag("P2").gameObject;
    }

    void Update()
    {
        if (currentQuest == 1){
            playerDetectorQuest1.SetActive(true);
        } 
        else if (currentQuest == 2)
        {
            playerDetectorQuest2.SetActive(false);
            playerDetectorQuest2.SetActive(true);
        }
        else
        {
            playerDetectorQuest1.SetActive(false);
            playerDetectorQuest2.SetActive(false);
        }
    }
}
