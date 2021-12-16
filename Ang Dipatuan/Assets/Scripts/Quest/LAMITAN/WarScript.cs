using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class WarScript : MonoBehaviour
{
    public QuestUI quest;

    QuestGoldGiver questGoldGiver;
    GameObject[] enemyLocomotionManger;
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

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        box = GameObject.FindGameObjectWithTag("TELEPORT").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        questChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker>();
        warSpawner = GameObject.FindGameObjectWithTag("WarSpawner");
        warSpawner.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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

    public void AcceptQuest2()
    {
        if (quest.currentQuest == 1)
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

    private void Update()
    {
        if (quest.goal.IsReached())
        {
            timer += Time.deltaTime;
            questGoldGiver.QuestComplete(quest.goldReward);
            quest.goal.currentAmount = 0;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            questChecker.questNum = quest.currentQuest;
            warSpawner.SetActive(false);
            questChecker.SaveStatQuest();
            ctr += 1;
            gameSceneScript.FadeToScene(3);
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
