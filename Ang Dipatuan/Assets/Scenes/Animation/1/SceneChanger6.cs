using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger6 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    float time;
    SaveQuestScript saveQuestScript;

    private void Awake()
    {
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 15.5f)
        {
            time = 0f;
            FadeToScene(13);
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        saveQuestScript.eightQuest = true;
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
