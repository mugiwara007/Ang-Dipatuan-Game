using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameSceneScript3 gameSceneScript3;
    SaveQuestScript saveQuestScript;
    Image cbtn;
    int sceneToLoad;
    public Text continueTxt;

    private void Awake()
    {
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        cbtn = GameObject.FindGameObjectWithTag("ContinueBtn").GetComponent<Image>();
    }

    private void Start()
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            cbtn.enabled = false;
            continueTxt.color = Color.gray;
        }
        else
        {
            cbtn.enabled = true;
            continueTxt.color = Color.white;
        }
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }

    public void StartGame()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
