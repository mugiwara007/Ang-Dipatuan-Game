using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveQuestScript : MonoBehaviour
{
    public static SaveQuestScript Instance;

    public int CurrQuest;
    public int questPos;
    public bool isLoadActive = false;
    public bool secondQuest = false;
    public bool thirdQuest = false;

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
