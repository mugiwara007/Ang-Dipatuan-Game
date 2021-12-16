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
        warSpawner = GameObject.FindGameObjectWithTag("WarSpawner");
        warSpawner.SetActive(false);
    }

    private void FixedUpdate()
    {
        Debug.Log("OUT :" + quest.goal.IsReached());
        if (quest.goal.IsReached())
        {
            Debug.Log("Warscript :" + quest.goal.IsReached());
            questGoldGiver.QuestComplete(quest.goldReward);
            quest.goal.currentAmount = 0;
            quest.currentQuest += 1;
            questComplete.SetActive(true);
            questChecker.questNum = quest.currentQuest;
            warSpawner.SetActive(false);
            questChecker.SaveStatQuest();
            ctr = 1;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log(questChecker.questNum);
            if (saveQuestScript.CurrQuest == 1)
            {
                Debug.Log("Savescript: " + saveQuestScript.CurrQuest);
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
            Debug.Log("Savescript: "+saveQuestScript.CurrQuest);
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
