using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
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
    WaypointScript waypointScript;
    public GameObject questComplete;
    private float timer = 0f;
    private int ctr = 0;
    BoxCollider box;
    public GameObject training;
    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;
    QuestChecker questChecker;
    SaveQuestScript saveQuestScript;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        box = GameObject.FindGameObjectWithTag("Activator").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        questChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker>();
        training.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (saveQuestScript.CurrQuest == 0)
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

    public void AcceptQuest1()
    {
        if (saveQuestScript.CurrQuest == 0)
        {
            box.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = quest.desc;
            waypointScript.target = waypoint.transform;
            waypoint.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            waypointScript.enabled = true;
            training.SetActive(true);
            movement.stun = false;
            cinemachineBrain.enabled = true;
        }
        
    }

    private void Update()
    {
        if (quest.goal.IsReached() && ctr == 0)
        {
            timer += Time.deltaTime;
            questGoldGiver.QuestComplete(quest.goldReward);
            quest.goal.currentAmount = 0;
            quest.currentQuest = saveQuestScript.CurrQuest;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            questChecker.questNum = quest.currentQuest;
            questChecker.SaveStatQuest();
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
                gameSceneScript.FadeToScene(3);
            }
        }
    }
}
