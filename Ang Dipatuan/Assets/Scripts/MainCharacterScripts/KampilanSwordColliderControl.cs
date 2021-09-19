using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KampilanSwordColliderControl : MonoBehaviour
{
    GameObject kampilanSword;

    void Awake()
    {
        kampilanSword = GameObject.FindGameObjectWithTag("KampilanArmed");
    }

    private void Update()
    {
        kampilanSword = GameObject.FindGameObjectWithTag("KampilanArmed");
    }

    public void ActivateKampilanSwordCollider()
    {
        kampilanSword.GetComponent<CapsuleCollider>().enabled = true;
    }

    public void DisableKampilanSwordCollider()
    {
        kampilanSword.GetComponent<CapsuleCollider>().enabled = false;
    }
}
