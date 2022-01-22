using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class PauseInGame : MonoBehaviour
{
    CinemachineBrain cinemachineBrain;
    GameObject pauseMenu;
    MainCharacterController movement;
    GameObject pauseUI;
    GameObject optionUI;
    GameObject exitVerUI;
    GameObject updater;
    GameSceneScript gameSceneScript;
    GameSceneScript2 gameSceneScript2;

    private void Awake()
    {
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        pauseUI = GameObject.FindGameObjectWithTag("PauseBtn");
        optionUI = GameObject.FindGameObjectWithTag("OptionMenu");
        exitVerUI = GameObject.FindGameObjectWithTag("ExitGame");
        updater = GameObject.FindGameObjectWithTag("Updater");
        
        try
        {
            gameSceneScript = GameObject.FindGameObjectWithTag("G1").GetComponent<GameSceneScript>();
        } catch
        {
            gameSceneScript2 = GameObject.FindGameObjectWithTag("G2").GetComponent<GameSceneScript2>();
        }
    }

    public void QuitGame()
    {
        Destroy(updater);
        try
        {
            gameSceneScript.FadeToScene(0);
        } catch
        {
            gameSceneScript2.FadeToScene(0);
        }
    }

    public void CancelQuit()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        exitVerUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        //cinemachineBrain.enabled = true;
        movement.stun = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //Time.timeScale = 1;
    }

    public void OptionMenu()
    {
        pauseUI.SetActive(false);
        optionUI.SetActive(true);
    }

    public void ExitGame()
    {
        pauseUI.SetActive(false);
        exitVerUI.SetActive(true);
    }

    public void BackOption()
    {
        pauseUI.SetActive(true);
        optionUI.SetActive(false);
    }
}
