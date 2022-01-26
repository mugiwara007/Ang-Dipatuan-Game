using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger7 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    float time;
    SaveQuestScript saveQuestScript;

    void Update()
    {
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        time += Time.deltaTime;
        if (time >= 4f)
        {
            time = 0f;
            FadeToScene(6);
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        saveQuestScript.fourthQuest = true;
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
