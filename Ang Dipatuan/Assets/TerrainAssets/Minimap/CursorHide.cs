using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHide : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
