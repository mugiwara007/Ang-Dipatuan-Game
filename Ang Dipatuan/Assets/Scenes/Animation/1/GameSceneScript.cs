using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneScript : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    public int qctr;
    QuestChecker questChecker;

    private void Awake()
    {
        questChecker = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker>();
    }

    public void FadeToScene(int sceneIndex)
    {
        qctr += 1;
        questChecker.quest2 = qctr;
        questChecker.SavePos();
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
