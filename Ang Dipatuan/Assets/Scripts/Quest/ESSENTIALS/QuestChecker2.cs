using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool canTeleport4 = true;
    private bool canTeleport5 = true;
    float timer = 0f;
    float time = 0f;
    bool ifDone1 = true;
    bool ifDone2 = true;
    bool isEntered = false;
    public bool avocadoController = false;
    GameObject[] avocadoSpawner;
    GameObject waypointMarker;
    WaypointScript waypointScript;
    Quest5Script quest5Script;
    Quest6Script quest6Script;
    GameObject noEntryCollider1;
    GameObject noEntryCollider2;
    GameObject noEntryDetector1;
    GameObject noEntryDetector2;
    GameObject noEntryCollider3;
    GameObject noEntryDetector3;
    Quest7Script quest7Script;
    GameObject quest7Collider;
    GameObject quest8ColliderObj;
    GameObject finalWarObject;
    GameObject quest9Collider;
    Quest8Script quest8Script;
    Quest9Script quest9Script;

    public GameObject sky1;
    public GameObject sky2;
    public GameObject light1;
    public GameObject light2;

    bool isDone = true;

    PlayerBar player;
    Gold gold;
    Inventory inventory;
    ClotheinInventory clothes;

    Image activeSkill2;
    Image activeSkill3;

    GameObject unlockSkill2;
    GameObject unlockSkill2UI;
    GameObject unlockSkill3;
    GameObject unlockSkill3UI;

    BraveryMode braveryMode;
    DipatuanMode dipatuanMode;

    public GameObject SkillsTextToType1;
    public GameObject SkillsTextToType3;

    GameSceneScript2 gameSceneScript2;
    SaveQuestScript saveQuestScript;

    Quest4Script quest4;
    Quest5Script quest5;
    Quest6Script quest6;
    Quest7Script quest7;

    public GameObject youDiedUI;

    GameObject holdUI;

    Text questDesc;

    //Sound FX (Environment)
    public AudioSource rain;

    public void SaveStatQuest2()
    {
        SaveQuestScript.Instance.CurrQuest = questNum;
    }

    public void SavePos2()
    {
        SaveQuestScript.Instance.questPos = quest2;
    }

    void Start()
    {
        Instantiate2();
        quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        quest5Script.quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        quest6Script.quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        quest7Script.quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        quest8Script.quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        quest9Script.quest.currentQuest = SaveQuestScript.Instance.CurrQuest;
        gameSceneScript2.qctr = SaveQuestScript.Instance.questPos;

        if (saveQuestScript.CurrQuest > 3)
        {
            activeSkill2.enabled = true;
        }

        if (saveQuestScript.CurrQuest > 5)
        {
            activeSkill3.enabled = true;
        }

        if (saveQuestScript.CurrQuest == 8)
        {
            SaveSystem.SavePlayer(player, gold, saveQuestScript, inventory, clothes);
        }

        if (saveQuestScript.isLoadActive == true)
        {
            PlayerData data = SaveSystem.LoadPlayer();
            player.health = data.health;
            player.mana = data.mana;

            gold.total_gold = data.playerGold;

            saveQuestScript.CurrQuest = data.currentQuest;

            inventory.coconut = data.coconut;
            inventory.avocado = data.avocado;
            inventory.pineapple = data.pineapple;
            inventory.fruitbasket = data.fruitBasket;

            clothes.boughtCloth1 = data.armor1;
            clothes.boughtCloth2 = data.armor2;
            clothes.boughtCloth3 = data.armor3;

            saveQuestScript.secondQuest = data.Save2;
            saveQuestScript.thirdQuest = data.Save3;
            saveQuestScript.fourthQuest = data.Save4;
            saveQuestScript.seventhQuest = data.Save5;
            saveQuestScript.eightQuest = data.Save6;
            saveQuestScript.lastQuest = data.lquest;

            Vector3 position;

            position.x = data.position[0];
            position.y = data.position[1];
            position.z = data.position[2];

            saveQuestScript.quest4Accepted = data.q4Accepted;
            saveQuestScript.quest5Accepted = data.q5Accepted;
            saveQuestScript.quest6Accepted = data.q6Accepted;
            saveQuestScript.quest7Accepted = data.q7Accepted;

            isEntered = true;

            StartCoroutine(activateCharController(position));

            if (saveQuestScript.CurrQuest == 4)
            {
                ifDone1 = false;
                canTeleport1 = false;
            }

            if (saveQuestScript.CurrQuest == 5)
            {
                canTeleport2 = false;
            }

            if (saveQuestScript.CurrQuest == 6)
            {
                ifDone2 = false;
                canTeleport3 = false;
            }

            if (saveQuestScript.CurrQuest > 6)
            {
                foreach (GameObject spawn in avocadoSpawner)
                {
                    spawn.SetActive(true);
                }
            }

            if (saveQuestScript.CurrQuest == 7)
            {
                canTeleport5 = false;
            }

            if (saveQuestScript.quest4Accepted == true)
            {
                quest4.AcceptQuest4();
            }

            if (saveQuestScript.quest5Accepted == true)
            {
                quest5.AcceptQuest5();
            }

            if (saveQuestScript.quest6Accepted == true)
            {
                quest6.AcceptQuest6();
            }

            if (saveQuestScript.quest7Accepted == true)
            {
                quest7.AcceptQuest7();
            }

            if (saveQuestScript.CurrQuest == 8)
            {
                saveQuestScript.lastQuest = data.lquest;
            }

            saveQuestScript.isLoadActive = false;
        }
    }

    void Update()
    {
        if (saveQuestScript.CurrQuest == 8 && quest8Script.quest.isActive == false)
        {
            if(player.health == 0)
            {
                timer += Time.deltaTime;
                youDiedUI.SetActive(true);
                if (timer > 3f)
                {
                    youDiedUI.SetActive(false);
                    timer = 0f;
                    saveQuestScript.isLoadActive = true;
                    gameSceneScript2.FadeToScene(6);
                }
            }
        }

        if (saveQuestScript.CurrQuest == 4)
        {
            if (player.health == 0)
            {
                timer += Time.deltaTime;
                youDiedUI.SetActive(true);
                if (timer > 3f)
                {
                    youDiedUI.SetActive(false);
                    timer = 0f;
                    saveQuestScript.isLoadActive = true;
                    gameSceneScript2.FadeToScene(6);
                }
            }
        }

        if (saveQuestScript.CurrQuest == 3)
        {
            if (isDone == true)
            {
                questDesc.text = "Save the game by using the campfire and go to the waypoint.";
                isDone = false;
            }
            unlockSkill2.SetActive(true);
            unlockSkill3.SetActive(true);
            quest4Collider.SetActive(true);
            quest4Waypoint.SetActive(true);
        }
        else if (saveQuestScript.CurrQuest == 4)
        {
            if (saveQuestScript.fourthQuest == true)
            {
                unlockSkill3.SetActive(true);
            }
            

            if (ifDone1 == true && saveQuestScript.fourthQuest == true)
            {
                holdUI.SetActive(true);
                waypointMarker.SetActive(false);
                unlockSkill2UI.SetActive(true);
            }

            time += Time.deltaTime;
            if (time > 4f && ifDone1 == true)
            {
                holdUI.SetActive(false);
                unlockSkill2UI.SetActive(false);
                timer = 0f;
                ifDone1 = false;
                movement.stun = true;
            }

            
            quest4Waypoint.SetActive(false);
            quest4Collider.SetActive(false);
            timer += Time.deltaTime;

            if (canTeleport1 == true && saveQuestScript.fourthQuest == true)
            {
                StartCoroutine("activateCharController1");
                canTeleport1 = false;
                movement.stun = true;
            }

            if (time >= 4)
            {
                movement.stun = false;
                quest5Collider.SetActive(true);
                quest5Waypoint.SetActive(true);
                isEntered = true;
            }
        }
        else if (saveQuestScript.CurrQuest == 5)
        {
            unlockSkill3.SetActive(true);
            quest6Detector.SetActive(true);

            if (canTeleport2 == true && isEntered == false)
            {
                StartCoroutine("activateCharController2");
                canTeleport2 = false;
            }
        }
        else if (saveQuestScript.CurrQuest == 6)
        {
            quest6Detector.SetActive(false);
            quest6Waypoint1.SetActive(false);
            quest6Waypoint2.SetActive(false);

            foreach (GameObject spawn in avocadoSpawner)
            {
                spawn.SetActive(true);
            }

            if (ifDone2 == true)
            {
                holdUI.SetActive(true);
                waypointMarker.SetActive(false);
                unlockSkill3UI.SetActive(true);
                movement.stun = true;
            }

            time += Time.deltaTime;
            if (time > 4f && ifDone2 == true)
            {
                holdUI.SetActive(false);
                unlockSkill3UI.SetActive(false);
                time = 0f;
                ifDone2 = false;
            }

            timer += Time.deltaTime;
            if (canTeleport3 == true && saveQuestScript.seventhQuest == true)
            {
                StartCoroutine("activateCharController3");
                canTeleport3 = false;
                movement.stun = true;
            }

            if (timer >= 4)
            {
                movement.stun = false;
                quest7Collider.SetActive(true);
                timer = 0f;
            }
        }
        else if (saveQuestScript.CurrQuest == 7)
        {
            if (canTeleport5 == true && saveQuestScript.eightQuest == true)
            {
                StartCoroutine("activateCharController5");
                questDesc.text = "Gather items and upgrade your armor before continuing. Go to the waypoint.";
                waypointScript.target = quest8ColliderObj.transform;
                canTeleport5 = false;
            }
            quest7Collider.SetActive(false);
            noEntryCollider1.SetActive(false);
            noEntryCollider2.SetActive(false);
            noEntryDetector1.SetActive(false);
            noEntryDetector2.SetActive(false);
            noEntryCollider3.SetActive(false);
            noEntryDetector3.SetActive(false);

            foreach (GameObject spawn in avocadoSpawner)
            {
                spawn.SetActive(true);
            }

            
        }
        else if (saveQuestScript.CurrQuest == 8)
        {
            if (canTeleport4 == true && saveQuestScript.lastQuest == true)
            {
                StartCoroutine("activateCharController4");
                if (!rain.isPlaying)
                {
                    rain.Play();
                }
                sky1.SetActive(false);
                light1.SetActive(false);
                storm.SetActive(true);
                sky2.SetActive(true);
                light2.SetActive(true);
                waypointMarker.SetActive(false);
                finalWarObject.SetActive(true);
                movement.stun = true;
                canTeleport4 = false;
                SaveSystem.SavePlayer(player, gold, saveQuestScript, inventory, clothes);
            }
            foreach (GameObject spawn in avocadoSpawner)
            {
                spawn.SetActive(true);
            }

            timer += Time.deltaTime;

            if (timer >= 3)
            {
                movement.stun = false;
                quest9Collider.SetActive(true);
                timer = 0f;
            }
        }

    }

    IEnumerator activateCharController(Vector3 pos)
    {
        //set this to false so that you can teleport the position of character
        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = false;
        GameObject.FindGameObjectWithTag("Player").transform.position = pos;

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>().enabled = true;

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

    IEnumerator activateCharController4()
    {
        //set this to false so that you can teleport the position of character
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(464.89f, 85.16f, 1198.21f);
        transform.Rotate(0.0f, 180.0f, 0.0f, Space.Self);

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<CharacterController>().enabled = true;

    }

    IEnumerator activateCharController5()
    {
        //set this to false so that you can teleport the position of character
        gameObject.GetComponent<CharacterController>().enabled = false;
        transform.position = new Vector3(636.93f, 101.81f, 4179.14f);
        transform.Rotate(0.0f, 90.0f, 0.0f, Space.Self);

        //after 0.7 seconds Enable char controller again
        yield return new WaitForSeconds(0.4f);

        gameObject.GetComponent<CharacterController>().enabled = true;

    }

    private void Instantiate2()
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
        avocadoSpawner = GameObject.FindGameObjectsWithTag("AvocadoSpawner");
        waypointMarker = GameObject.FindGameObjectWithTag("Waypont");
        quest5Script = GameObject.FindGameObjectWithTag("Quest5Collider").GetComponent<Quest5Script>();
        quest6Script = GameObject.FindGameObjectWithTag("Quest6NPC").GetComponent<Quest6Script>();
        quest7Script = GameObject.FindGameObjectWithTag("Quest7Collider").GetComponent<Quest7Script>();
        quest8Script = GameObject.FindGameObjectWithTag("Quest8Collider").GetComponent<Quest8Script>();
        quest9Script = GameObject.FindGameObjectWithTag("Quest9Collider").GetComponent<Quest9Script>();
        quest7Collider = GameObject.FindGameObjectWithTag("Quest7Collider");
        quest6Object = GameObject.FindGameObjectWithTag("Quest6NPC");
        noEntryCollider1 = GameObject.FindGameObjectWithTag("NoEntryCollider1");
        noEntryCollider2 = GameObject.FindGameObjectWithTag("NoEntryCollider2");
        noEntryDetector1 = GameObject.FindGameObjectWithTag("NoEntryDetector1");
        noEntryDetector2 = GameObject.FindGameObjectWithTag("NoEntryDetector2");
        noEntryCollider3 = GameObject.FindGameObjectWithTag("NoEntryCollider3");
        noEntryDetector3 = GameObject.FindGameObjectWithTag("NoEntryDetector3");
        quest6Detector = quest6Object.transform.Find("Player Detector").gameObject;
        waypointScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<WaypointScript>();
        quest8ColliderObj = GameObject.FindGameObjectWithTag("Quest8Collider");
        activeSkill2 = GameObject.FindGameObjectWithTag("BraverySkillYellowImage").GetComponent<Image>();
        activeSkill3 = GameObject.FindGameObjectWithTag("DipatuanSkillYellowImage").GetComponent<Image>();
        unlockSkill2 = GameObject.FindGameObjectWithTag("UnlockSkill2");
        unlockSkill3 = GameObject.FindGameObjectWithTag("UnlockSkill3");
        unlockSkill2UI = GameObject.FindGameObjectWithTag("Unlock2ndSkillUI");
        unlockSkill3UI = GameObject.FindGameObjectWithTag("Unlock3rdSkillUI");
        braveryMode = GameObject.FindGameObjectWithTag("Player").GetComponent<BraveryMode>();
        dipatuanMode = GameObject.FindGameObjectWithTag("Player").GetComponent<DipatuanMode>();
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        finalWarObject = GameObject.FindGameObjectWithTag("WAR");
        quest9Collider = GameObject.FindGameObjectWithTag("Quest9Collider");
        questDesc = GameObject.FindGameObjectWithTag("QuestUI").GetComponent<Text>();

        quest4 = GameObject.FindGameObjectWithTag("Quest4Collider").GetComponent<Quest4Script>();
        quest5 = GameObject.FindGameObjectWithTag("Quest5Collider").GetComponent<Quest5Script>();
        quest6 = GameObject.FindGameObjectWithTag("Quest6NPC").GetComponent<Quest6Script>();
        quest7 = GameObject.FindGameObjectWithTag("Quest7Collider").GetComponent<Quest7Script>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
        gold = GameObject.FindGameObjectWithTag("Player").GetComponent<Gold>();
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        clothes = GameObject.FindGameObjectWithTag("Player").GetComponent<ClotheinInventory>();

        holdUI = GameObject.FindGameObjectWithTag("HoldUI");
        holdUI.SetActive(false);

        gold.total_gold = saveQuestScript.gold;

        quest4Waypoint.SetActive(false);
        quest4Collider.SetActive(false);
        quest5Waypoint.SetActive(false);
        quest5Collider.SetActive(false);
        quest6Waypoint1.SetActive(false);
        quest6Waypoint2.SetActive(false);
        quest6Detector.SetActive(false);
        quest7Collider.SetActive(false);
        quest9Collider.SetActive(false);
        storm.SetActive(false);
        foreach (GameObject spawn in avocadoSpawner)
        {
            spawn.SetActive(false);
        }
        finalWarObject.SetActive(false);

        activeSkill2.enabled = false;
        activeSkill3.enabled = false;
        unlockSkill2.SetActive(false);
        unlockSkill2UI.SetActive(false);
        unlockSkill3.SetActive(false);
        unlockSkill3UI.SetActive(false);

        sky1.SetActive(true);
        light1.SetActive(true);
        sky2.SetActive(false);
        light2.SetActive(false);

        SkillsTextToType3.SetActive(false);
        SkillsTextToType1.SetActive(false);

        rain.playOnAwake = false;

        if (avocadoController == true)
        {
            foreach (GameObject spawn in avocadoSpawner)
            {
                spawn.SetActive(true);
            }
        }

        if (saveQuestScript.CurrQuest >= 7)
        {
            noEntryCollider1.SetActive(false);
            noEntryCollider2.SetActive(false);
            noEntryDetector1.SetActive(false);
            noEntryDetector2.SetActive(false);
            noEntryCollider3.SetActive(false);
            noEntryDetector3.SetActive(false);
        }

        if (saveQuestScript.CurrQuest >= 4)
        {
            activeSkill2.enabled = true;
        }

        if (saveQuestScript.CurrQuest >= 6)
        {
            activeSkill3.enabled = true;
        }
    }
}
