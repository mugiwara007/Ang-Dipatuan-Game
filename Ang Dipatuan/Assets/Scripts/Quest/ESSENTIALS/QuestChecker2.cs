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
    GameObject quest6Object;
    GameObject quest6Detector;
    GameObject quest6Waypoint1;
    GameObject quest6Waypoint2;
    MainCharacterController movement;
    GameObject storm;
    private bool canTeleport1 = true;
    private bool canTeleport2 = true;
    private bool canTeleport3 = true;
    float timer = 0f;
    float time = 0f;
    bool ifDone1 = true;
    bool ifDone2 = true;
    public bool avocadoController = false;
    GameObject avocadoSpawner;
    GameObject waypointMarker;
    WaypointScript waypointScript;
    Quest5Script quest5Script;
    Quest6Script quest6Script;
    GameObject noEntryCollider1;
    GameObject noEntryCollider2;
    GameObject noEntryDetector1;
    GameObject noEntryDetector2;
    Quest7Script quest7Script;
    GameObject quest7Collider;
    GameObject quest8ColliderObj;

    GameObject activeSkill2;
    GameObject activeSkill3;

    GameObject unlockSkill2;
    GameObject unlockSkill2UI;
    GameObject unlockSkill3;
    GameObject unlockSkill3UI;

    BraveryMode braveryMode;
    DipatuanMode dipatuanMode;

    GameSceneScript2 gameSceneScript2;

    private void Awake()
    {
        gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
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
        quest7Script = GameObject.FindGameObjectWithTag("Quest7Collider").GetComponent<Quest7Script>();
        quest7Collider = GameObject.FindGameObjectWithTag("Quest7Collider");
        quest6Object = GameObject.FindGameObjectWithTag("Quest6NPC");
        noEntryCollider1 = GameObject.FindGameObjectWithTag("NoEntryCollider1");
        noEntryCollider2 = GameObject.FindGameObjectWithTag("NoEntryCollider2");
        noEntryDetector1 = GameObject.FindGameObjectWithTag("NoEntryDetector1");
        noEntryDetector2 = GameObject.FindGameObjectWithTag("NoEntryDetector2");
        quest6Detector = quest6Object.transform.Find("Player Detector").gameObject;
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        quest8ColliderObj = GameObject.FindGameObjectWithTag("Quest8Collider");
        activeSkill2 = GameObject.FindGameObjectWithTag("BraverySkillYellowImage");
        activeSkill3 = GameObject.FindGameObjectWithTag("DipatuanSkillYellowImage");
        unlockSkill2 = GameObject.FindGameObjectWithTag("UnlockSkill2");
        unlockSkill3 = GameObject.FindGameObjectWithTag("UnlockSkill3");
        unlockSkill2UI = GameObject.FindGameObjectWithTag("Unlock2ndSkillUI");
        unlockSkill3UI = GameObject.FindGameObjectWithTag("Unlock3rdSkillUI");
        braveryMode = GameObject.FindGameObjectWithTag("Player").GetComponent<BraveryMode>();
        dipatuanMode = GameObject.FindGameObjectWithTag("Player").GetComponent<DipatuanMode>();

        quest4Waypoint.SetActive(false);
        quest4Collider.SetActive(false);
        quest5Waypoint.SetActive(false);
        quest5Collider.SetActive(false);
        quest6Waypoint1.SetActive(false);
        quest6Waypoint2.SetActive(false);
        quest6Detector.SetActive(false);
        quest7Collider.SetActive(false);
        storm.SetActive(false);
        avocadoSpawner.SetActive(false);

        activeSkill2.SetActive(false);
        activeSkill3.SetActive(false);
        unlockSkill2.SetActive(false);
        unlockSkill2UI.SetActive(false);
        unlockSkill3.SetActive(false);
        unlockSkill3UI.SetActive(false);

        braveryMode.enabled = false;
        dipatuanMode.enabled = false;

        if (avocadoController == true)
        {
            avocadoSpawner.SetActive(true);
        }

        if (quest.currentQuest >= 7)
        {
            noEntryCollider1.SetActive(false);
            noEntryCollider2.SetActive(false);
            noEntryDetector1.SetActive(false);
            noEntryDetector2.SetActive(false);
        }
        
        if (quest.currentQuest >= 4)
        {
            braveryMode.enabled = true;
            activeSkill2.SetActive(true);
        }

    }

    public void SaveStatQuest2()
    {
        SaveQuestScript2.Instance.CurrQuest2 = questNum;
    }

    public void SavePos2()
    {
        SaveQuestScript2.Instance.questPos2 = quest2;
    }

    void Start()
    {
        quest.currentQuest = SaveQuestScript2.Instance.CurrQuest2;
        quest5Script.quest.currentQuest = SaveQuestScript2.Instance.CurrQuest2;
        quest6Script.quest.currentQuest = SaveQuestScript2.Instance.CurrQuest2;
        quest7Script.quest.currentQuest = SaveQuestScript2.Instance.CurrQuest2;
        gameSceneScript2.qctr = SaveQuestScript2.Instance.questPos2;
    }

    void Update()
    {
        if (quest.currentQuest == 3)
        {
            unlockSkill2.SetActive(true);
            unlockSkill3.SetActive(true);
            quest4Collider.SetActive(true);
            quest4Waypoint.SetActive(true);
        }
        else if (quest.currentQuest == 4)
        {
            braveryMode.enabled = true;
            activeSkill2.SetActive(true);
            unlockSkill3.SetActive(true);

            if (ifDone1 == true)
            {
                waypointMarker.SetActive(false);
                unlockSkill2UI.SetActive(true);
            }

            time += Time.deltaTime;
            if (time > 4f && ifDone1 == true)
            {
                unlockSkill2UI.SetActive(false);
                timer = 0f;
                ifDone1 = false;
            }

            movement.stun = true;
            quest4Waypoint.SetActive(false);
            quest4Collider.SetActive(false);
            timer += Time.deltaTime;
            if (canTeleport1)
            {
                StartCoroutine("activateCharController1");
                canTeleport1 = false;
            }

            if (time >= 4)
            {
                movement.stun = false;
                quest5Collider.SetActive(true);
                quest5Waypoint.SetActive(true);
            }
        }
        else if (quest.currentQuest == 5)
        {
            quest6Detector.SetActive(true);
            if (canTeleport2)
            {
                StartCoroutine("activateCharController2");
                canTeleport2 = false;
            }
        }
        else if (quest.currentQuest == 6)
        {
            quest6Detector.SetActive(false);
            quest6Waypoint1.SetActive(false);
            quest6Waypoint2.SetActive(false);
            avocadoSpawner.SetActive(true);
            timer += Time.deltaTime;
            if (canTeleport3)
            {
                StartCoroutine("activateCharController3");
                canTeleport3 = false;
            }

            if (timer >= 2)
            {
                quest7Collider.SetActive(true);
                timer = 0f;
            }
        }
        else if (quest.currentQuest == 7)
        {
            quest7Collider.SetActive(false);
            noEntryCollider1.SetActive(false);
            noEntryCollider2.SetActive(false);
            noEntryDetector1.SetActive(false);
            noEntryDetector2.SetActive(false);
            waypointScript.target = quest8ColliderObj.transform;


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

    IEnumerator activateCharController2()
    {
        //set this to false so that you can teleport the position of character
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(621.37f, 104.26f, 4169.60f);
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<CharacterController>().enabled = true;

    }

    IEnumerator activateCharController3()
    {
        //set this to false so that you can teleport the position of character
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(621.37f, 104.26f, 4169.60f);
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<CharacterController>().enabled = true;

    }
}
