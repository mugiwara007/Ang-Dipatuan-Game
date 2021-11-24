using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC2 : MonoBehaviour
{
    GameObject canvas;

    private void Awake()
    {
        canvas = gameObject.transform.Find("Canvas").gameObject;
    }

    private void Start()
    {
        canvas.SetActive(false);
    }
}
