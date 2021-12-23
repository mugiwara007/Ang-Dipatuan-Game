using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetector2 : MonoBehaviour
{
    GameObject canvas;
    private bool canAccept2;
    Quest6Script quest6;

    private void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Quest6E");
        canAccept2 = false;
        quest6 = GameObject.FindGameObjectWithTag("Quest6NPC").GetComponent<Quest6Script>();
    }

    private void Update()
    {
        if (canAccept2)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.Confined;
                quest6.Quest6();
            }
        }
    }






    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAccept2 = true;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canAccept2 = false;
            canvas.SetActive(false);
        }
    }
}
