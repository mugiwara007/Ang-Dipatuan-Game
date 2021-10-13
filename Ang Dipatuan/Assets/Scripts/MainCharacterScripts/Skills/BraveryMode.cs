using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveryMode : MonoBehaviour
{
    Image skillColorYellow;

    private bool onCooldown;

    public bool isBraveryModeActivated;

    private void Awake()
    {
        isBraveryModeActivated = false;
        skillColorYellow = GameObject.FindGameObjectWithTag("BraverySkillYellowImage").GetComponent<Image>();
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("BraveryMode") && onCooldown == false)
        {
            isBraveryModeActivated = true;

            skillColorYellow.fillAmount = 0;
            onCooldown = true;

            StartCoroutine("braveryModeTurnOff");
        }

        if (onCooldown)
        {
            skillColorYellow.fillAmount = skillColorYellow.fillAmount += 0.02f * Time.deltaTime;

            if (skillColorYellow.fillAmount >= 1f)
            {
                onCooldown = false;
            }
        }


    }

    IEnumerator braveryModeTurnOff()
    {
        yield return new WaitForSeconds(5f);
        isBraveryModeActivated = false;

    }
}
