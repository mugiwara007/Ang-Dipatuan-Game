using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Quest5Script : MonoBehaviour
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
    private float timer = 0f;
    private int ctr = 0;
    public GameObject newWaypoint;
    BoxCollider box;
    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;
    QuestChecker2 questChecker2;
    SaveQuestScript saveQuestScript;
    GameObject waypointMarker;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        box = GameObject.FindGameObjectWithTag("Quest5Collider").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (saveQuestScript.CurrQuest == 4)
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

    public void AcceptQuest5()
    {
        if (saveQuestScript.CurrQuest == 4)
        {
            saveQuestScript.quest5Accepted = true;
            waypointMarker.SetActive(true);
            box.enabled = false;
            questWindow.SetActive(false);
            quest.isActive = true;
            questDesc.text = "Follow the Waypoint to the Village.";
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

        if (quest.goal.IsReached())
        {
            if (saveQuestScript.CurrQuest == 4 && ctr == 0)
            {
                timer += Time.deltaTime;
                saveQuestScript.quest5Accepted = false;
                questGoldGiver.QuestComplete(quest.goldReward);
                saveQuestScript.gold += quest.goldReward;
                quest.goal.currentAmount = 0;
                quest.currentQuest = saveQuestScript.CurrQuest;
                quest.currentQuest += 1;
                questComplete.SetActive(true);
                questChecker2.questNum = quest.currentQuest;
                questChecker2.SaveStatQuest2();
                questChecker2.FruitSaver();
                ctr += 1;
                waypointScript.enabled = true;
                waypointScript.target = newWaypoint.transform;
            }
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
