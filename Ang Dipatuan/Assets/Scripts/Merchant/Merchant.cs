using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
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
