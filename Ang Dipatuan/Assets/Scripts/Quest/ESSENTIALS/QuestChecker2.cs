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
    MainCharacterController movement;
    GameObject storm;
    private bool canTeleport1 = true;
    float time;

    GameSceneScript gameSceneScript;

    private void Awake()
    {
        gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        storm = GameObject.FindGameObjectWithTag("Storm");
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        quest4Collider = GameObject.FindGameObjectWithTag("Quest4Collider");
        quest4Waypoint = GameObject.FindGameObjectWithTag("Waypoint1");
        quest4Waypoint.SetActive(false);
        quest4Collider.SetActive(false);
        storm.SetActive(false);
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
        if (quest.currentQuest == 3)
        {
            storm.SetActive(false);
            Debug.Log("wow");
            quest4Collider.SetActive(true);
            quest4Waypoint.SetActive(true);
        }
        else if (quest.currentQuest == 4)
        {
            if (canTeleport1)
            {
                StartCoroutine("activateCharController1");
                canTeleport1 = false;
            }
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
