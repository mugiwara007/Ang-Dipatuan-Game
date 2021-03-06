using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger13 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    SaveQuestScript saveQuestScript;
    float time;

    private void Awake()
    {
        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 14f)
        {
            time = 0f;
            FadeToScene(10);
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        saveQuestScript.lastQuest = true;
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
