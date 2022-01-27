using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDescScript : MonoBehaviour
{
    GameObject skillDesc;

    // Start is called before the first frame update
    void Start()
    {
        skillDesc = GameObject.FindGameObjectWithTag("SkillDesc");

        skillDesc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            skillDesc.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.F1))
        {
            skillDesc.SetActive(false);
        }
    }
}
