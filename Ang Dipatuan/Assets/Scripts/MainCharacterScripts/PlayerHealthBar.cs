using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    private Text healthText;
    private Image healthBar;

    float health, maxHealth = 100;

    float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        healthText = GameObject.FindGameObjectWithTag("HealthBarText").GetComponent<Text>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBarImage").GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + " / 100";

        lerpSpeed = 3f * Time.deltaTime; // value to How Smooth transition of healthBar 


        //to Avoid health go higher than 100
        if(health > maxHealth)
        {
            health = maxHealth;
        }


        //Avoid health going lower than 0 or negative value
        if (health < 0)
        {
            health = 0;
        }

        //change the health of the player
        HealthBarFiller();

        //Change the color of HealthBar
        ColorChanger();
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    public void ColorChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

        healthBar.color = healthColor;
    }

    public void Damage(float damagePoints)
    {
        if(health > 0)
        {
            health -= damagePoints;
        }
    }

    public void Heal(float healingPoints)
    {
        if (health < maxHealth)
        {
            health += healingPoints;
        }
    }

}
