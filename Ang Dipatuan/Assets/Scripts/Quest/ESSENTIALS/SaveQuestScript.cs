using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveQuestScript : MonoBehaviour
{
    public static SaveQuestScript Instance;

    public int CurrQuest;
    public int questPos;
    public int gold = 0;
    public bool isLoadActive = false;
    public bool secondQuest = false;
    public bool thirdQuest = false;
    public bool fourthQuest = false;

    public bool quest4Accepted = false;
    public bool quest5Accepted = false;
    public bool quest6Accepted = false;
    public bool quest7Accepted = false;
    public bool quest8Accepted = false;
    public bool quest9Accepted = false;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}
