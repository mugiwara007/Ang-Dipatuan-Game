using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrisSwordColliderControl : MonoBehaviour
{
    GameObject krisSword;

    void Awake()
    {
        krisSword = GameObject.FindGameObjectWithTag("KrisDagger");
    }

    private void Update()
    {
        krisSword = GameObject.FindGameObjectWithTag("KrisDagger");
    }

    public void ActivateKrisDaggerCollider()
    {
        krisSword.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void DisableKrisDaggerCollider()
    {
        krisSword.GetComponent<CapsuleCollider>().enabled = false;
    }
}
