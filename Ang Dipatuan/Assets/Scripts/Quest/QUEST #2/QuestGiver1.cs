using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver1 : MonoBehaviour
{
    public QuestUI quest;

    //public PlayerBar player;

    QuestGoldGiver questGoldGiver;

    public Text questDesc;

    public GameObject questWindow;
    //public GameObject waypoint;
    public GameObject kill;
    public Text titleText;
    public Text descriptionText;
    public Text goldText;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
    }

    public void Quest1()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.desc;
        goldText.text = quest.goldReward.ToString();
        goldText.text += " Gold";
    }

    public void AcceptQuest()
    {
        questWindow.SetActive(false);
        quest.isActive = true;
        //player.quest = quest;
        questDesc.text = quest.desc;
        //waypoint.SetActive(true);
        kill.SetActive(true);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (quest.goal.IsReached())
        {
            questGoldGiver.QuestComplete(quest.goldReward);
            quest.goal.currentAmount = 0;
        }
    }
}
