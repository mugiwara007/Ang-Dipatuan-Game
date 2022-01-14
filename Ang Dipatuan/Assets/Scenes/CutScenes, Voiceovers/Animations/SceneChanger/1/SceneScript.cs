using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneScript : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;
    public GameObject skip;
    float time;
    //float skipTimer;

    private void Awake()
    {
        skip.SetActive(false);
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >= 84f)
        {
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
