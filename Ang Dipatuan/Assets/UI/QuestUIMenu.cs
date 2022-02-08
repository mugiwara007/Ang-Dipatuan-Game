using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIMenu : MonoBehaviour
{
    public GameObject questUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (questUI != null)
            {
                Animator animator = questUI.GetComponent<Animator>();
                if (animator != null)
                {
                    bool isOpen = animator.GetBool("open");

                    animator.SetBool("open", !isOpen);
                }
            }
        }
    }
}
