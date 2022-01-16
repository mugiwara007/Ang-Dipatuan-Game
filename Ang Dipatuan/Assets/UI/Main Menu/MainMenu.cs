using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    public GameSceneScript3 gameSceneScript3;
    SaveQuestScript saveQuestScript;
    int sceneToLoad;

    private void Awake()
    {
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void StartGame()
    {
        gameSceneScript3.FadeToScene(1);
    }

    public void LoadGame()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        saveQuestScript.isLoadActive = true;
        sceneToLoad = data.sceneIndex;
        Debug.Log(sceneToLoad);
        gameSceneScript3.FadeToScene(sceneToLoad);
    }
}
