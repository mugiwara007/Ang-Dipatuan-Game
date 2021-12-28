using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneScript2 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    public int qctr;
    QuestChecker2 questChecker2;

    private void Awake()
    {
        questChecker2 = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestChecker2>();
    }

    public void FadeToScene(int sceneIndex)
    {
        qctr += 1;
        questChecker2.quest2 = qctr;
        questChecker2.SavePos();
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
