using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger2 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    float time;
    SaveQuestScript SaveQ;

    void Start() {
        SaveQ = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 33f)
        {
            SaveQ.secondQuest = true;
            time = 0f;
            FadeToScene(2);
        }
    }

    public void FadeToScene(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }


}
