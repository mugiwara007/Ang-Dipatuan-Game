using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGoldGiver : MonoBehaviour
{
    Gold gold;

    private void Awake()
    {
        gold = gameObject.GetComponent<Gold>();
    }

    public void QuestComplete(int goldReward)
    {
        gold.addGold(goldReward);
    }
}
