using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBar : CharacterStats
{
    private Text healthText;
    private Image healthBar, staminaBar, healthBarMax, manaBar;

    float waitTime = 0f;


    float lerpSpeed, staminaLerpSpeed;

    public float mana, maxMana = 100;

    public bool pineappleEaten = false;

    private void Awake()
    {
        maxMana = 100;
        mana = maxMana;

        pineappleEaten = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        stamina = maxStamina;

        

        healthText = GameObject.FindGameObjectWithTag("HealthBarText").GetComponent<Text>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBarImage").GetComponent<Image>();
        healthBarMax = GameObject.FindGameObjectWithTag("HealthBarImageMax").GetComponent<Image>();


        staminaBar = GameObject.FindGameObjectWithTag("StaminaBarImage").GetComponent<Image>();

        manaBar = GameObject.FindGameObjectWithTag("ManaBar").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = health + " / " + maxHealth;

        lerpSpeed = 3f * Time.deltaTime; // value to How Smooth transition of healthBar 

        staminaLerpSpeed = 50f * Time.deltaTime; // value to How Smooth transition of staminaBar 

        //to Avoid health go higher than 100
        if (health > maxHealth)
        {
            health = maxHealth;
        }


        //Avoid health going lower than 0 or negative value
        if (health < 0)
        {
            health = 0;
        }


        //to Avoid Mana go higher than 100
        if (mana > maxMana)
        {
            mana = maxMana;
        }


        //Avoid mana going lower than 0 or negative value
        if (mana < 0)
        {
            mana = 0;
        }


        //change the health of the player
        HealthBarFiller();

        //Change the color of HealthBar
        ColorChanger();

        //change the health of the player
        StaminaBarFiller();

        //change the health of the player
        ManaBarFiller();

        if(pineappleEaten == true)
        {
            stamina = maxStamina;
        }


    }
    public void ManaBarFiller()
    {
        manaBar.fillAmount = Mathf.Lerp(manaBar.fillAmount, mana / maxMana, 50f * Time.deltaTime);
    }
    public void ReduceMana(float reducePoints)
    {
        if (mana > 0)
        {
            mana -= reducePoints;
        }
    }

    public void AddMana(float addPoints)
    {
        if (mana < maxHealth)
        {
            mana += addPoints;
        }
    }

    public void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health / maxHealth, lerpSpeed);
    }

    public void StaminaBarFiller()
    {
        MainCharacterController player = GetComponent<MainCharacterController>();
        

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }

        if(stamina <= 0)
        {
            stamina = 0;
        }


        //when player is running decrease stamina
        if (player.isRunning)
        {
            stamina -= Time.deltaTime * 7f;
            waitTime = 0f;
        }
        //when player is not running decrease stamina
        if (!player.isRunning)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= 2f)
            {
                stamina += Time.deltaTime * 15f;
            }
        }

        staminaBar.fillAmount = Mathf.Lerp(staminaBar.fillAmount, stamina / maxStamina, staminaLerpSpeed);
    }

    public void DecreaseStamina(float decreasePoints)
    {
        if (stamina > 0)
        {
            stamina -= decreasePoints;
            waitTime = 0f;
        }
    }

    public void ColorChanger()
    {
        if (health >= maxHealth)
        {
            healthBarMax.enabled = false;
        }
        else
        {
            healthBarMax.enabled = true;
        }

        Color32 darkGreen = new Color32(22, 120, 6, 255);
        Color32 darkRed = new Color32(97, 10, 11, 255);

        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));

        Color maxHealthColor = Color.Lerp(darkRed, darkGreen, (health / maxHealth));

        healthBar.color = healthColor;
        healthBarMax.color = maxHealthColor;

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

    public void MaxManaHealth()
    {
            health = maxHealth;

            mana = maxMana;
    }

    public void enableMaxStamina()
    {
        pineappleEaten = true;
    }

    public void disableMaxStamina()
    {
        pineappleEaten = false;
    }
}
