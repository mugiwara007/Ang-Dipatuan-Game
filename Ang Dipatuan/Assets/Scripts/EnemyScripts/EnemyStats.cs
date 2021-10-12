using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour
{

    public float ehealth, emaxHealth = 100;
    private Image healthBar;
    float lerpSpeed;


    void Start()
    {
        emaxHealth = 100;
        ehealth = emaxHealth;

        healthBar = gameObject.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>();
    }

    public void Damage(float dmgPoint)
    {
        if (ehealth > 0)
        {
            ehealth -= dmgPoint;
        }
    }

    private void Update()
    {
        lerpSpeed = 50f * Time.deltaTime; // value to How Smooth transition of healthBar 

        //to Avoid health go higher than 100
        if (ehealth > emaxHealth)
        {
            ehealth = emaxHealth;
        }


        //Avoid health going lower than 0 or negative value
        if (ehealth < 0)
        {
            ehealth = 0;
        }

        HealthBarFiller();

        if (ehealth <= 0)
        {
            StartCoroutine("destroyEnemy");
        }
    }

    IEnumerator destroyEnemy()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, ehealth / emaxHealth, lerpSpeed);
    }
}