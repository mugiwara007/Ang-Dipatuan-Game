using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Quest9Script : MonoBehaviour
{
    public QuestUI quest;

    QuestGoldGiver questGoldGiver;
    GameSceneScript2 gameSceneScript2;

    public Text questDesc;

    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text goldText;
    public GameObject questComplete;
    public GameObject questFailed;
    private float timer = 0f;
    private int ctr = 0;
    BoxCollider box;
    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;
    QuestChecker2 questChecker2;
    GameObject warSpawner;
    SaveQuestScript2 saveQuestScript2;
    PlayerBar player;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        box = GameObject.FindGameObjectWithTag("Quest9Collider").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
        saveQuestScript2 = GameObject.FindGameObjectWithTag("Updater2").GetComponent<SaveQuestScript2>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        warSpawner = GameObject.FindGameObjectWithTag("WarSpawner");
        warSpawner.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (quest.goal.IsReached())
        {
            questGoldGiver.QuestComplete(quest.goldReward);
            quest.goal.currentAmount = 0;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            questChecker2.questNum = quest.currentQuest;
            warSpawner.SetActive(false);
            questChecker2.SaveStatQuest2();
            ctr = 1;
            gameSceneScript2.FadeToScene(10);
        }
        if (ctr == 1)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                questComplete.SetActive(false);
                timer = 0f;
                ctr = 0;
            }
        }
        if (player.health == 0)
        {
            timer += Time.deltaTime;
            questFailed.SetActive(true);
            if (timer > 3f)
            {
                questFailed.SetActive(false);
                timer = 0f;
                gameSceneScript2.FadeToScene(1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (saveQuestScript2.CurrQuest2 == 1)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                questWindow.SetActive(true);
                titleText.text = quest.title;
                descriptionText.text = quest.desc;
                goldText.text = quest.goldReward.ToString();
                goldText.text += " Gold";
                movement.stun = true;
                cinemachineBrain.enabled = false;
            }
        }
    }

    public void AcceptQuest2()
    {
        if (saveQuestScript2.CurrQuest2 == 8)
        {
            box.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = quest.desc;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            movement.stun = false;
            cinemachineBrain.enabled = true;
            warSpawner.SetActive(true);
        }

    }
}
