using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;

    public int requiredAmount;
    public int currentAmount;

    public bool IsReached()
    {
        return (currentAmount >= requiredAmount);
    }

    public void EnemyKilled()
    {

    }

    public void Waypoint()
    {
        if (goalType == GoalType.Waypoint)
        {
            currentAmount++;
        } 
    }

}

public enum GoalType
{
    Kill,
    Gathering,
    Talk,
    Waypoint
}
