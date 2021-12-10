using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherQuest1 : MonoBehaviour
{
    public GameObject gather1;

    public GameObject Item;

    QuestGiver1 questGiver;

    private void Awake()
    {
        questGiver = GameObject.FindGameObjectWithTag("Q1").GetComponent<QuestGiver1>();
        InstantiateEnemies();
    }

    private void InstantiateEnemies()
    {
        var item = Instantiate(Item, gameObject.transform.position, gameObject.transform.rotation);
        item.transform.parent = gameObject.transform;
    }


    // Update is called once per frame
    void Update()
    {
        if (IsItemCollected())
        {
            gather1.SetActive(false);
            questGiver.quest.goal.ItemGathered();
        }


    }

    public bool IsItemCollected()
    {
        return gameObject.transform.childCount == 0;
    }
}
