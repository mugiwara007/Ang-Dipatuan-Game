using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestChecker : MonoBehaviour
{
    public QuestUI quest;
    public int questNum;
    public int quest2;

    TutorialScript tutorialScript;

    public GameObject waypoint;
    GameObject activator;
    GameObject enemyCampSpawn2;
    GameObject enemyCampSpawn3;
    GameObject enemyEscape;
    GameObject enemyCamp2;
    GameObject war;
    GameObject storm;
    BoxCollider actBox;
    GameObject escapeObj;
    GameObject escapeCollider;
    MainCharacterController movement;
    GameObject warCollider;
    float time;

    private bool canTeleport1 = true;
    private bool canTeleport2 = true;

    GameSceneScript gameSceneScript;

    private void Awake()
    {
        enemyCampSpawn2 = GameObject.FindGameObjectWithTag("E2");
        enemyCampSpawn3 = GameObject.FindGameObjectWithTag("E3");
        enemyEscape = GameObject.FindGameObjectWithTag("E4");
        enemyCamp2 = GameObject.FindGameObjectWithTag("EC2");
        tutorialScript = GameObject.FindGameObjectWithTag("Player").GetComponent<TutorialScript>();
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        storm = GameObject.FindGameObjectWithTag("Storm");
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        escapeObj = GameObject.FindGameObjectWithTag("EscapeObj");
        escapeCollider = GameObject.FindGameObjectWithTag("EscapeCollider");
        actBox = GameObject.FindGameObjectWithTag("Activator").GetComponent<BoxCollider>();
        warCollider = GameObject.FindGameObjectWithTag("TELEPORT");
        war = GameObject.FindGameObjectWithTag("WAR");
        activator = GameObject.FindGameObjectWithTag("Activator");
        war.SetActive(false);
        actBox.enabled = true;
        escapeObj.SetActive(false);
        storm.SetActive(false);
        enemyCampSpawn2.SetActive(false);
        enemyCampSpawn3.SetActive(false);
        enemyCamp2.SetActive(false);
        enemyEscape.SetActive(false);
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
        gameSceneScript.qctr = SaveQuestScript.Instance.questPos;
    }

    void Update()
    {
        
        if (quest.currentQuest == 0)
        {
            activator.SetActive(true);
        }
        else if (quest.currentQuest == 1 && gameSceneScript.qctr >= 1)
        {
            warCollider.SetActive(false);
            war.SetActive(true);
            waypoint.SetActive(false);
            movement.stun = true;
            time += Time.deltaTime;

            //So that the Coroutine will only be called once and in the next frame canTeleport will be false and will not go in this statement
            if (canTeleport1)
            {
                StartCoroutine("activateCharController1");
                canTeleport1 = false;
            }
            if (time >= 3)
            {
                movement.stun = false;
                warCollider.SetActive(true);
            }

            tutorialScript.enabled = false;
            enemyCamp2.SetActive(true);

        }
        else if (quest.currentQuest == 2)
        {
            tutorialScript.enabled = false;
            escapeObj.SetActive(true);
            movement.stun = true;
            time += Time.deltaTime;

            if (canTeleport2)
            {
                StartCoroutine("activateCharController2");
                canTeleport2 = false;
            }
            if (time >= 3)
            {
                movement.stun = false;
                enemyEscape.SetActive(true);
                escapeCollider.SetActive(true);
            }

            waypoint.SetActive(true);

            enemyCampSpawn2.SetActive(true);
            enemyCampSpawn3.SetActive(true);
        }
    }

    IEnumerator activateCharController1()
    {
        //set this to false so that you can teleport the position of character
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(662.59f, 82.83f, 755.55f);

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<CharacterController>().enabled = true;

    }

    IEnumerator activateCharController2()
    {
        //set this to false so that you can teleport the position of character
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(713.16f, 84.58f, 597.21f);
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<CharacterController>().enabled = true;

    }
}
