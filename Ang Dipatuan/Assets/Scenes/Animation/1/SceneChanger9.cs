using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger9 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    float time;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 7f)
        {
            time = 0f;
            FadeToScene(10);
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
