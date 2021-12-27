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
    GameSceneScript gameSceneScript;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        box = GameObject.FindGameObjectWithTag("PlayerDetector1").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
    }

    public void Quest6()
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

    public void AcceptQuest7()
    {
        if (saveQuestScript.CurrQuest == 6)
        {
            waypointMarker.SetActive(true);
            box.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = quest.desc;
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
            if (player.health == 0)
            {
                timer += Time.deltaTime;
                questFailed.SetActive(true);
                if (timer > 3f)
                {
                    questFailed.SetActive(false);
                    timer = 0f;
                    gameSceneScript.FadeToScene(7);
                }
            }
        }

        if (quest.goal.IsReached())
        {
            timer += Time.deltaTime;
            questGoldGiver.QuestComplete(quest.goldReward);
            quest.goal.currentAmount = 0;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            Debug.Log("HELLO");
            questChecker2.questNum = quest.currentQuest;
            questChecker2.SaveStatQuest();
            ctr += 1;
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
    }
}
