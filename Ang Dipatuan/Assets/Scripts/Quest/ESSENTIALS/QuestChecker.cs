using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestChecker : MonoBehaviour
{
    public QuestUI quest;
    public int questNum;
    public int quest2;
    public Transform player;
    public Transform target;

    TutorialScript tutorialScript;

    public GameObject waypoint;
    public GameObject activator;
    GameObject playerDetectorQuest1;
    GameObject playerDetectorQuest2;
    GameObject enemyCampSpawn2;
    GameObject enemyCampSpawn3;
    GameObject enemyCamp2;
    public GameObject war;

    GameSceneScript gameSceneScript;

    private void Awake()
    {
        playerDetectorQuest1 = GameObject.FindGameObjectWithTag("P1");
        playerDetectorQuest2 = GameObject.FindGameObjectWithTag("P2");
        enemyCampSpawn2 = GameObject.FindGameObjectWithTag("E2");
        enemyCampSpawn3 = GameObject.FindGameObjectWithTag("E3");
        enemyCamp2 = GameObject.FindGameObjectWithTag("EC2");
        tutorialScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TutorialScript>();
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
    }

    public void SaveStatQuest()
    {
        Debug.Log("Waasuup "+questNum);
        SaveQuestScript.Instance.CurrQuest = questNum;
    }

    public void SavePos()
    {
        Debug.Log("Dayum " + quest2);
        SaveQuestScript.Instance.questPos = quest2;
    }

    void Start()
    {
        quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        gameSceneScript.qctr = SaveQuestScript.Instance.questPos;
    }

    void Update()
    {
        if (quest.currentQuest == 0)
        {
            activator.SetActive(true);
            enemyCampSpawn2.SetActive(false);
            enemyCampSpawn3.SetActive(false);
            enemyCamp2.SetActive(false);
        }
        else if (quest.currentQuest == 1 && gameSceneScript.qctr >= 1)
        {
            war.SetActive(true);
            waypoint.SetActive(false);
            player.transform.position = target.position;
            tutorialScript.enabled = false;
            enemyCamp2.SetActive(true);
        }
        else if (quest.currentQuest == 2)
        {
            waypoint.SetActive(true);
            enemyCampSpawn2.SetActive(true);
            enemyCampSpawn3.SetActive(true);
            playerDetectorQuest1.SetActive(true);
        }
    }
}
