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

    private void Awake()
    {
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        pauseUI = GameObject.FindGameObjectWithTag("PauseBtn");
        optionUI = GameObject.FindGameObjectWithTag("OptionMenu");
        exitVerUI = GameObject.FindGameObjectWithTag("ExitGame");
    }

    public void QuitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }

    public void CancelQuit()
    {
        exitVerUI.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        cinemachineBrain.enabled = true;
        movement.stun = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
