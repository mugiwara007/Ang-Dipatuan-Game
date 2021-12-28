using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoEntryScript : MonoBehaviour
{
    public GameObject canvas;
    private bool canEnter;

    private void Awake()
    {
        canvas.SetActive(false);
        canEnter = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canEnter = true;
            canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            canEnter = false;
            canvas.SetActive(false);
        }
    }
}
