using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver1 : MonoBehaviour
{
    public QuestUI quest;

    public PlayerBar player;

    public Text questDesc;


    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text goldText;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
    }

    public void Quest1()
    {
        Debug.Log("IT WORKED");
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
        player.quest = quest;
        questDesc.text = quest.desc;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
