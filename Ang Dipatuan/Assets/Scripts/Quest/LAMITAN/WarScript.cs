using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class WarScript : MonoBehaviour
{
    public QuestUI quest;

    QuestGoldGiver questGoldGiver;
    GameSceneScript gameSceneScript;

    public Text questDesc;

    public GameObject questWindow;
    public GameObject waypoint;
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
    QuestChecker questChecker;
    GameObject warSpawner;
    SaveQuestScript saveQuestScript;
    PlayerBar player;

    private bool qfailed = true;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        box = GameObject.FindGameObjectWithTag("TELEPORT").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        questChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
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
            quest.currentQuest =2;
            questComplete.SetActive(true);
            questChecker.questNum = quest.currentQuest;
            warSpawner.SetActive(false);
            questChecker.SaveStatQuest();
            ctr = 1;
            
        }
        if (ctr == 1)
        {
            timer += Time.deltaTime;
            if (timer > 3f)
            {
                questComplete.SetActive(false);
                timer = 0f;
                ctr = 0;
                gameSceneScript.FadeToScene(4);
            }
        }
        if(player.health == 0 && qfailed == true)
        {
            timer += Time.deltaTime;
            questFailed.SetActive(true);
            if (timer > 3f)
            {
                questFailed.SetActive(false);
                timer = 0f;
                gameSceneScript.FadeToScene(2);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(questChecker.questNum);
            if (saveQuestScript.CurrQuest == 1)
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
        if (saveQuestScript.CurrQuest == 1)
        {
            box.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = quest.desc;
            waypoint.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            movement.stun = false;
            cinemachineBrain.enabled = true;
            warSpawner.SetActive(true);
        }

    }

}
