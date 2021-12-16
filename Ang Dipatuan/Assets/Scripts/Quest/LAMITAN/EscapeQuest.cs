using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class EscapeQuest : MonoBehaviour
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
    WaypointScript waypointScript;
    public GameObject questComplete;
    public GameObject questFailed;
    private float timer = 0f;
    private int ctr = 0;
    BoxCollider box;
    MainCharacterController movement;
    CinemachineBrain cinemachineBrain;
    QuestChecker questChecker;
    EnemyLocomotionManger locomotionManager;

    private void Awake()
    {
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();
        questGoldGiver = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestGoldGiver>();
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        box = GameObject.FindGameObjectWithTag("EscapeCollider").GetComponent<BoxCollider>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        questChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker>();
        enemyLocomotionManger = GameObject.FindGameObjectsWithTag("Enemy");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
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

    public void AcceptQuest2()
    {
        if (quest.currentQuest == 0)
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
            movement.stun = false;
            cinemachineBrain.enabled = true;
            Debug.Log("HEEYYY");
        }
        
    }

    private void Update()
    {
        enemyLocomotionManger = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var enemy in enemyLocomotionManger)
        {
            locomotionManager = enemy.GetComponent<EnemyLocomotionManger>();
            if (locomotionManager.currentTarget != null)
            {
                questFailed.SetActive(true);
                timer += Time.deltaTime;
                if (timer > 3f)
                {
                    questFailed.SetActive(false);
                    timer = 0f;
                    gameSceneScript.FadeToScene(1);
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
            questChecker.questNum = quest.currentQuest;
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
