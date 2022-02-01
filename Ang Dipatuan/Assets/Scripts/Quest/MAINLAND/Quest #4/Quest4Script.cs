using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest4Script : MonoBehaviour
{
    public QuestUI quest;

    QuestGoldGiver questGoldGiver;

    GameSceneScript2 gameSceneScript2;

    public Text questDesc;

    public AudioSource QFX;

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
    GameObject quest4EnemySpawner;
    public GameObject wall;
    PlayerBar player;
    GameObject waypointMarker;

    private bool qfailed = true;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        box = GameObject.FindGameObjectWithTag("Quest4Collider").GetComponent<BoxCollider>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
        quest4EnemySpawner = GameObject.FindGameObjectWithTag("Quest4EnemySpawner");
        quest4EnemySpawner.SetActive(false);
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
        wall.SetActive(false);
        QFX.playOnAwake = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (saveQuestScript.CurrQuest == 3)
            {
                waypointScript.enabled = false;
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

    public void AcceptQuest4()
    {
        if (saveQuestScript.CurrQuest == 3)
        {
            saveQuestScript.quest4Accepted = true;
            box.enabled = false;
            wall.SetActive(true);
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = "Make your way to the Waypoint.";
            waypointScript.target = waypoint.transform;
            waypoint.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            quest4EnemySpawner.SetActive(true);
            waypointScript.enabled = true;
            movement.stun = false;
            cinemachineBrain.enabled = true;
            QFX.Play();
        }
    }

    private void Update()
    {
        if (saveQuestScript.CurrQuest == 3)
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
                    gameSceneScript2.FadeToScene(6);
                }
            }
        }

        if (quest.goal.IsReached())
        {
            timer += Time.deltaTime;
            questGoldGiver.QuestComplete(quest.goldReward);
            saveQuestScript.gold += quest.goldReward;
            saveQuestScript.quest4Accepted = false;
            quest.goal.currentAmount = 0;
            quest.currentQuest = saveQuestScript.CurrQuest;
            quest.currentQuest += 1;
            Debug.Log(quest.currentQuest);
            questComplete.SetActive(true);
            questChecker2.questNum = quest.currentQuest;
            wall.SetActive(false);
            quest4EnemySpawner.SetActive(false);
            questChecker2.SaveStatQuest2();
            gameSceneScript2.FadeToScene(12);
            waypointMarker.SetActive(false);
            QFX.Stop();
        }
    }
}
