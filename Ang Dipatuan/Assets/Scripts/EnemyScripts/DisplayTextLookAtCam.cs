using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayTextLookAtCam : MonoBehaviour
{
    private Camera cameraTransform;

    void Awake()
    {
        cameraTransform = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + cameraTransform.transform.rotation * Vector3.forward, cameraTransform.transform.rotation * Vector3.up);
    }
}
