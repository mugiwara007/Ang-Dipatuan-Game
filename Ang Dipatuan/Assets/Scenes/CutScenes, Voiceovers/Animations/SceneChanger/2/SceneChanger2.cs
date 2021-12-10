using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger2 : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    float time;

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 34f)
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
