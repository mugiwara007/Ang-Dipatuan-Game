using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger3 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    float time;
    //float skipTimer;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 41f)
        {
            time = 0f;
            FadeToScene(1);
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
