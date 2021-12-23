using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestChecker2 : MonoBehaviour
{
    public QuestUI quest;
    public int questNum;
    public int quest2;

    public GameObject waypoint;
    GameObject quest4Collider;
    GameObject quest4Waypoint;
    GameObject quest5Collider;
    GameObject quest5Waypoint;
    GameObject quest6Waypoint1;
    GameObject quest6Waypoint2;
    MainCharacterController movement;
    GameObject storm;
    private bool canTeleport1 = true;
    float time;
    public bool avocadoController = false;
    GameObject avocadoSpawner;
    GameObject waypointMarker;
    Quest5Script quest5Script;
    Quest6Script quest6Script;


    GameSceneScript gameSceneScript;

    private void Awake()
    {
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        storm = GameObject.FindGameObjectWithTag("Storm");
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        quest4Collider = GameObject.FindGameObjectWithTag("Quest4Collider");
        quest4Waypoint = GameObject.FindGameObjectWithTag("Waypoint1");
        quest5Collider = GameObject.FindGameObjectWithTag("Quest5Collider");
        quest5Waypoint = GameObject.FindGameObjectWithTag("Waypoint2");
        quest6Waypoint1 = GameObject.FindGameObjectWithTag("Waypoint3");
        quest6Waypoint2 = GameObject.FindGameObjectWithTag("Waypoint4");
        avocadoSpawner = GameObject.FindGameObjectWithTag("AvocadoSpawner");
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
        quest5Script = GameObject.FindGameObjectWithTag("Quest5Collider").GetComponent<Quest5Script>();
        quest6Script = GameObject.FindGameObjectWithTag("Quest6NPC").GetComponent<Quest6Script>();
        quest4Waypoint.SetActive(false);
        quest4Collider.SetActive(false);
        quest5Waypoint.SetActive(false);
        quest5Collider.SetActive(false);
        quest6Waypoint1.SetActive(false);
        quest6Waypoint2.SetActive(false);
        storm.SetActive(false);
        avocadoSpawner.SetActive(false);

        if (avocadoController == true)
        {
            avocadoSpawner.SetActive(true);
        }
    }

    public void SaveStatQuest()
    {
        SaveQuestScript.Instance.CurrQuest = questNum;
    }

    public void SavePos()
    {
        SaveQuestScript.Instance.questPos = quest2;
    }

    void Start()
    {
        quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        quest5Script.quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        quest6Script.quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        gameSceneScript.qctr = SaveQuestScript.Instance.questPos;
    }

    void Update()
    {
        if (quest.currentQuest == 3)
        {
            quest4Collider.SetActive(true);
            quest4Waypoint.SetActive(true);
        }
        else if (quest.currentQuest == 4)
        {
            quest4Waypoint.SetActive(false);
            quest4Collider.SetActive(false);
            quest5Collider.SetActive(true);
            quest5Waypoint.SetActive(true);
            if (canTeleport1)
            {
                StartCoroutine("activateCharController1");
                canTeleport1 = false;
            }
        }
        else if (quest.currentQuest == 5)
        {
            
        }
        else if (quest.currentQuest == 6)
        {
            avocadoSpawner.SetActive(true);
        }

    }

    IEnumerator activateCharController1()
    {
        //set this to false so that you can teleport the position of character
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(1780.47f, 89.91f, 3106.57f);
        transform.Rotate(0.0f, -0.8f, 0.0f, Space.Self);

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<CharacterController>().enabled = true;

    }
}
