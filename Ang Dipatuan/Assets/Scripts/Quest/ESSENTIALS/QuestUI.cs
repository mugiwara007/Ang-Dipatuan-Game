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
    public int currentQuest = 0;

    public QuestGoal goal;
}
