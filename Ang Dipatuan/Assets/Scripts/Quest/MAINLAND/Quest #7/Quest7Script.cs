using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest7Script : MonoBehaviour
{
    public QuestUI quest;

    QuestGoldGiver questGoldGiver;

    public Text questDesc;

    public GameObject questWindow;
    public GameObject waypoint;
    public Text titleText;
    public Text descriptionText;
    public Text goldText;
    WaypointScript waypointScript;
    public GameObject questComplete;
    public GameObject questFailed;
    private float timer = 0f;
    private int ctr = 0;
    BoxCollider box;
    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;
    QuestChecker2 questChecker2;
    SaveQuestScript saveQuestScript;
    GameObject waypointMarker;
    PlayerBar player;
    GameSceneScript2 gameSceneScript2;
    private bool qfailed = true;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        box = GameObject.FindGameObjectWithTag("Quest7Collider").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (saveQuestScript.CurrQuest == 6)
            {
                waypointMarker.SetActive(false);
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

    public void AcceptQuest7()
    {
        if (saveQuestScript.CurrQuest == 6)
        {
            saveQuestScript.quest7Accepted = true;
            waypointMarker.SetActive(true);
            box.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = "Find and kill all enemies in their camp.";
            waypointScript.target = waypoint.transform;
            waypoint.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            movement.stun = false;
            cinemachineBrain.enabled = true;
        }

    }

    private void Update()
    {
        if (saveQuestScript.CurrQuest == 6)
        {
            if (player.health == 0 && qfailed == true)
            {
                timer += Time.deltaTime;
                questFailed.SetActive(true);
                if (timer > 3f && qfailed == true)
                {
                    questFailed.SetActive(false);
                    timer = 0f;
                    qfailed = false;
                    saveQuestScript.isLoadActive = true;
                    gameSceneScript2.FadeToScene(6);
                }
            }
        }

        if (quest.goal.IsReached())
        {
            timer += Time.deltaTime;
            saveQuestScript.quest7Accepted = false;
            questGoldGiver.QuestComplete(quest.goldReward);
            saveQuestScript.gold += quest.goldReward;
            quest.goal.currentAmount = 0;
            quest.currentQuest = saveQuestScript.CurrQuest;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            questChecker2.questNum = quest.currentQuest;
            questChecker2.SaveStatQuest2();
            gameSceneScript2.FadeToScene(8);
        }
    }
}
