using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveQuestScript2 : MonoBehaviour
{
    public static SaveQuestScript2 Instance;

    public int CurrQuest2;
    public int questPos2;

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
