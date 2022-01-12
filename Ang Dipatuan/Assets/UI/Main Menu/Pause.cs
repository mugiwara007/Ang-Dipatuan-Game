using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Pause : MonoBehaviour
{
    GameObject pauseMenu;
    CinemachineBrain cinemachineBrain;
    GameObject optionUI;
    GameObject exitVerUI;
    MainCharacterController movement;


    private bool pauseOpen = false;
    private bool isPaused = true;

    private void Start()
    {
        pauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
        optionUI = GameObject.FindGameObjectWithTag("OptionMenu");
        exitVerUI = GameObject.FindGameObjectWithTag("ExitGame");
        cinemachineBrain = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CinemachineBrain>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterController>();
        optionUI.SetActive(false);
        exitVerUI.SetActive(false);
        pauseMenu.SetActive(pauseOpen);
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseOpen = !pauseOpen;
            isPaused = !isPaused;
            movement.stun = pauseOpen;
            pauseMenu.SetActive(pauseOpen);
            cinemachineBrain.enabled = isPaused;
            Cursor.visible = pauseOpen;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}
