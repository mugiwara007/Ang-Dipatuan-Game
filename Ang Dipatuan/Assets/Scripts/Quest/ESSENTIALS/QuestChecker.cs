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
    float timer;
    bool ifDone = true;

    Image activeSkill1;
    GameObject unlockSkill1;
    GameObject unlock1stSkillUi;

    SlowMotionMode slowMotion;

    private bool canTeleport1 = true;
    private bool canTeleport2 = true;
    private bool canSave = true;

    GameSceneScript gameSceneScript;
    SaveQuestScript saveQuestScript;

    GameObject SkillsTextToType1;
    GameObject SkillsTextToType2;
    GameObject SkillsTextToType3;

    BraveryMode braveryMode;
    DipatuanMode dipatuanMode;

    PlayerBar player;
    Gold gold;
    Inventory inventory;
    ClotheinInventory clothes;

    

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
        InstantiateObjects();
        quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        gameSceneScript.qctr = SaveQuestScript.Instance.questPos;


        if (saveQuestScript.isLoadActive == true)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            saveQuestScript.secondQuest = data.Save2;
            saveQuestScript.thirdQuest = data.Save3;

            gold.total_gold = data.playerGold;

            saveQuestScript.CurrQuest = data.currentQuest;

            saveQuestScript.isLoadActive = false;
        }

    }

    void Update()
    {
        if (saveQuestScript.isLoadActive == false && canSave == true)
        {
            SaveSystem.SavePlayer(player, gold, saveQuestScript, inventory, clothes);
            canSave = false;
        }

        if (saveQuestScript.CurrQuest == 0)
        {
            activator.SetActive(true);
        }
        else if (saveQuestScript.CurrQuest == 1 && saveQuestScript.secondQuest == true)
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
        else if (saveQuestScript.CurrQuest == 2 && saveQuestScript.thirdQuest == true)
        {
            if (ifDone == true)
            {
                unlock1stSkillUi.SetActive(true);
            }
            
            timer += Time.deltaTime;
            if (timer > 4f && ifDone == true)
            {
                unlock1stSkillUi.SetActive(false);
                timer = 0f;
                ifDone = false;
            }
            activeSkill1.enabled = true;
            unlockSkill1.SetActive(false);
            slowMotion.enabled = true;
            tutorialScript.enabled = false;
            escapeObj.SetActive(true);
            movement.stun = true;
            time += Time.deltaTime;

            if (canTeleport2)
            {
                StartCoroutine("activateCharController2");
                canTeleport2 = false;
            }
            if (time >= 4)
            {
                movement.stun = false;
                escapeCollider.SetActive(true);
            }

            waypoint.SetActive(true);

            enemyCampSpawn2.SetActive(true);
            enemyCampSpawn3.SetActive(true);
        }
    }

    IEnumerator activateCharController(Vector3 pos)
    {
        //set this to false so that you can teleport the position of character
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;
        Debug.Log("LIPAT PWESTO");

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;

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

    private void InstantiateObjects()
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
        slowMotion = GameObject.FindGameObjectWithTag("Player").GetComponent<SlowMotionMode>();
        braveryMode = GameObject.FindGameObjectWithTag("Player").GetComponent<BraveryMode>();
        dipatuanMode = GameObject.FindGameObjectWithTag("Player").GetComponent<DipatuanMode>();
        escapeCollider.SetActive(false);
        unlockSkill1 = GameObject.FindGameObjectWithTag("UnlockSkill1");

        unlock1stSkillUi = GameObject.FindGameObjectWithTag("Unlock1stSkillUI");
        unlock1stSkillUi.SetActive(false);
        unlockSkill1.SetActive(true);
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<Gold>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        clothes = GameObject.FindGameObjectWithTag("Player").GetComponent<ClotheinInventory>();

        SkillsTextToType3 = GameObject.FindGameObjectWithTag("SkillsTextToType3");
        SkillsTextToType2 = GameObject.FindGameObjectWithTag("SkillsTextToType2");
        SkillsTextToType1 = GameObject.FindGameObjectWithTag("SkillsTextToType1");

        activeSkill1 = GameObject.FindGameObjectWithTag("SlowMoSkillYellowImage").GetComponent<Image>();
        activeSkill1.enabled = false;

        war.SetActive(false);
        actBox.enabled = true;
        escapeObj.SetActive(false);
        storm.SetActive(false);
        enemyCampSpawn2.SetActive(false);
        enemyCampSpawn3.SetActive(false);
        enemyCamp2.SetActive(false);
        enemyEscape.SetActive(false);
        SkillsTextToType3.SetActive(false);
        SkillsTextToType1.SetActive(false);
        slowMotion.enabled = false;
        braveryMode.enabled = false;
        dipatuanMode.enabled = false;
    }
}
