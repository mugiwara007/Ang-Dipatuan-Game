using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger5 : MonoBehaviour
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
        if (time >= 18f)
        {
            time = 0f;
            FadeToScene(6);
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        saveQuestScript.seventhQuest = true;
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
