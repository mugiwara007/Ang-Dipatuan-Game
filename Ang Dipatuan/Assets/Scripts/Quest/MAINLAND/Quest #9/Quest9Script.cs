using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class Quest9Script : MonoBehaviour
{
    public AudioSource QFX;

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
    GameObject finalWarSpawner;
    SaveQuestScript saveQuestScript;
    PlayerBar player;

    private bool qfailed = true;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        box = GameObject.FindGameObjectWithTag("Quest9Collider").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        finalWarSpawner = GameObject.FindGameObjectWithTag("FinalWarSpawner");
        finalWarSpawner.SetActive(false);
        QFX.playOnAwake = false;
    }

    private void FixedUpdate()
    {
        if (quest.goal.IsReached())
        {
            questGoldGiver.QuestComplete(quest.goldReward);
            saveQuestScript.gold += quest.goldReward;
            quest.goal.currentAmount = 0;
            quest.currentQuest = saveQuestScript.CurrQuest;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            questChecker2.questNum = quest.currentQuest;
            finalWarSpawner.SetActive(false);
            questChecker2.FruitSaver();
            questChecker2.SaveStatQuest2();
            ctr = 1;
            saveQuestScript.isLoadActive = true;
            gameSceneScript2.FadeToScene(11);
            QFX.Stop();
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
        if (player.health == 0 && qfailed == true)
        {
            timer += Time.deltaTime;
            questFailed.SetActive(true);
            if (timer > 3f)
            {
                questFailed.SetActive(false);
                timer = 0f;
                gameSceneScript2.FadeToScene(6);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (saveQuestScript.CurrQuest == 8)
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

    public void AcceptQuest9()
    {
        if (saveQuestScript.CurrQuest == 8)
        {
            box.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = "Defeat all waves of enemies.";
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            movement.stun = false;
            cinemachineBrain.enabled = true;
            finalWarSpawner.SetActive(true);
            QFX.Play();
        }

    }
}
