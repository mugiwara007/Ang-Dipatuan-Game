using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest6Script : MonoBehaviour
{
    public AudioSource QFX;

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
    PlayerDetector2 playerDetector;
    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;
    QuestChecker2 questChecker2;
    SaveQuestScript saveQuestScript;
    GameObject waypoint4;
    public int avocadoCtr;
    GameObject waypointMarker;
    GameSceneScript2 gameSceneScript2;
    PlayerBar player;
    Canvas canvas;

    private bool qfailed = true;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        playerDetector = GameObject.FindGameObjectWithTag("PlayerDetector1").GetComponent<PlayerDetector2>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
        waypoint4 = GameObject.FindGameObjectWithTag("Waypoint4");
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
        canvas = GameObject.FindGameObjectWithTag("Quest6E").GetComponent<Canvas>();
        QFX.playOnAwake = false;
    }

    public void Quest6() { 
            if (saveQuestScript.CurrQuest == 5)
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

    public void AcceptQuest6()
    {
        if (saveQuestScript.CurrQuest == 5)
        {
            saveQuestScript.quest6Accepted = true;
            waypointMarker.SetActive(true);
            playerDetector.enabled = false;
            canvas.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = "Kill all enemies in the camp and collect 3 Avocadoes.";
            waypoint.SetActive(true);
            waypointScript.target = waypoint.transform;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            movement.stun = false;
            cinemachineBrain.enabled = true;
            QFX.Play();
        }

    }

    private void Update()
    {
        if (saveQuestScript.CurrQuest == 5)
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

        if (saveQuestScript.CurrQuest == 5)
        {
            if (avocadoCtr == 3)
            {
                questDesc.text = "Return to the village and deliver the Avocadoes.";
                waypoint4.SetActive(true);
                waypointMarker.SetActive(true);
                waypointScript.target = waypoint4.transform;
            }
        }

        if (quest.goal.IsReached())
        {
            timer += Time.deltaTime;
            saveQuestScript.quest6Accepted = false;
            questGoldGiver.QuestComplete(quest.goldReward);
            saveQuestScript.gold += quest.goldReward;
            quest.goal.currentAmount = 0;
            quest.currentQuest = saveQuestScript.CurrQuest;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            questChecker2.questNum = quest.currentQuest;
            questChecker2.avocadoController = true;
            questChecker2.SaveStatQuest2();
            questChecker2.FruitSaver();
            gameSceneScript2.FadeToScene(7);
            QFX.Stop();
        }
    }
}
