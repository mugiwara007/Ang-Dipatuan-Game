using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DipatuanMode : MonoBehaviour
{
    Image skillColorYellow;

    private bool onCooldown;

    public DamageEnemy damageEnemyScript;

    public GameObject fireOnSword1, fireOnSword2, ember;

    private bool fireTurnOff = true;

    Animator anim;

    MainCharacterController charControl;

    public GameObject kampilan;

    //text that will pop up to type
    GameObject SkillsTextToType1;

    private int correctedLetters = 0;

    public bool typingMode = false;

    private bool isUnlocked2 = false;

    private string remainingWord = string.Empty;
    private string currentWord = "dipatuan";

    PlayerBar manaStats;

    private bool isUnlocked1;

    SaveQuestScript saveQuestScript;

    void Start()
    {
        skillColorYellow = GameObject.FindGameObjectWithTag("DipatuanSkillYellowImage").GetComponent<Image>();

        saveQuestScript = GameObject.FindGameObjectWithTag("Updater").GetComponent<SaveQuestScript>();
        manaStats = gameObject.GetComponent<PlayerBar>();
        try
        {
            //text that will pop up to type
            SkillsTextToType1 = GameObject.FindGameObjectWithTag("SkillsTextToType1");
        }
        catch
        {
            //Ignore error
        }

        SkillsTextToType1.SetActive(false);

        onCooldown = false;

        anim = gameObject.GetComponent<Animator>();
        charControl = gameObject.GetComponent<MainCharacterController>();

        SetCurrentWord();

       
    }

    // Update is called once per frame
    void Update()
    {
        if (saveQuestScript.CurrQuest > 5)
        {
            isUnlocked1 = true;
        }

        if (typingMode)
        {
            gameObject.GetComponent<SlowMotionMode>().enabled = false;
            gameObject.GetComponent<BraveryMode>().enabled = false;
            if (Input.anyKeyDown)
            {
                string keyPressed = Input.inputString;


                if (keyPressed.Length == 1)
                    EnterLetter(keyPressed);
            }
        }
        else
        {
            gameObject.GetComponent<SlowMotionMode>().enabled = true;
            gameObject.GetComponent<BraveryMode>().enabled = true;
        }



        //try
        //{
        //    kampilan = GameObject.FindGameObjectWithTag("KampilanArmed");

        //    damageEnemyScript = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();

        //}
        //catch
        //{
        //    //Ignore error
        //}


        if (fireTurnOff)
        {
            fireOnSword1.SetActive(true);
            fireOnSword2.SetActive(false);
            ember.SetActive(false);
        }

        if (manaStats.mana >= 5)
        {
            if (Input.GetButtonDown("DipatuanMode") && onCooldown == false && gameObject.GetComponent<BraveryMode>().typingMode == false && gameObject.GetComponent<SlowMotionMode>().typingMode == false)
            {
                if (isUnlocked1 == true)
                {
                    manaStats.ReduceMana(12);
                    setLetterColorToBlack();
                    StartCoroutine("StopTypingMode");
                    if (Time.timeScale == 1.0f)
                    {
                        Time.timeScale = 0.2f;
                    }

                    //Sets typing mode to true
                    typingMode = true;

                    //show text that will pop up to type 
                    try
                    {
                        SkillsTextToType1.SetActive(true);
                    }
                    catch
                    {
                        Debug.Log("Error DP MODE UPDATE");
                    }


                    StartCoroutine("StopTypingMode");
                }
            }
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

    private void SetCurrentWord()
    {
        remainingWord = currentWord;
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            SkillsTextToType1.transform.GetChild(correctedLetters).GetComponent<Text>().color = Color.green;
            correctedLetters++;

            RemoveLetter();

            //check if the remaining word length is 0 or empty
            if (IsWordComplete())
            {
                if (Time.timeScale == 0.2f)
                {
                    Time.timeScale = 1f;
                }

                correctedLetters = 0;
                ExecuteSkill();
                SetCurrentWord();
            }
        }
        else
        {
            typingMode = false;
            setLetterColorToRed();

            StartCoroutine("DelayedStop");
        }
    }

    IEnumerator DelayedStop()
    {
        yield return new WaitForSeconds(0.2f);

        correctedLetters = 0;

        SetCurrentWord();

        //set skill on cooldown
        skillColorYellow.fillAmount = 0;
        onCooldown = true;

        if (Time.timeScale == 0.2f)
        {
            Time.timeScale = 1f;
        }

        //Sets typing mode to false
        typingMode = false;

        //hides text that will pop up to type 
        SkillsTextToType1.SetActive(false);
    }

    IEnumerator StopTypingMode()
    {
        yield return new WaitForSeconds(0.7f);

        setLetterColorToRed();

        correctedLetters = 0;

        SetCurrentWord();

        //set skill on cooldown
        skillColorYellow.fillAmount = 0;
        onCooldown = true;

        if (Time.timeScale == 0.2f)
        {
            Time.timeScale = 1f;
        }

        //Sets typing mode to false
        typingMode = false;

        //hides text that will pop up to type 
        SkillsTextToType1.SetActive(false);
    }


    private void ExecuteSkill()
    {
        //Increases Damage
        damageEnemyScript.isDipatuanModeActivated = true;
        damageEnemyScript.dmgPts = 37f;

        //sets fire to kampilan
        fireOnSword1.SetActive(false);
        fireOnSword2.SetActive(true);
        ember.SetActive(true);
        fireTurnOff = false;

        //set skill on cooldown
        skillColorYellow.fillAmount = 0;
        onCooldown = true;

        //Display kampilan on hand
        kampilan.SetActive(true);

        //Activate animation for bravery mode
        anim.SetTrigger("dipatuanMode");

        //Stops ACtion of dipatuan when performing animation
        charControl.enabled = false;

        //Allow Dipatuan to move again
        StartCoroutine("moveAgain");

        //Turn Off dipatuan mode after 7 seconds
        StartCoroutine("DipatuanModeOff");

        //Sets typing mode to false
        typingMode = false;

        //hides text that will pop up to type 
        SkillsTextToType1.SetActive(false);
    }

    private bool IsCorrectLetter(string letter)
    {
        //0 because this returns the first letter of the remaining words
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
    }

    private bool IsWordComplete()
    {
        return remainingWord.Length == 0;
    }

    private void setLetterColorToRed()
    {
        SkillsTextToType1.transform.GetChild(0).GetComponent<Text>().color = Color.red;
        SkillsTextToType1.transform.GetChild(1).GetComponent<Text>().color = Color.red;
        SkillsTextToType1.transform.GetChild(2).GetComponent<Text>().color = Color.red;
        SkillsTextToType1.transform.GetChild(3).GetComponent<Text>().color = Color.red;
        SkillsTextToType1.transform.GetChild(4).GetComponent<Text>().color = Color.red;
        SkillsTextToType1.transform.GetChild(5).GetComponent<Text>().color = Color.red;
        SkillsTextToType1.transform.GetChild(6).GetComponent<Text>().color = Color.red;
        SkillsTextToType1.transform.GetChild(7).GetComponent<Text>().color = Color.red;
    }

    private void setLetterColorToBlack()
    {
        SkillsTextToType1.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        SkillsTextToType1.transform.GetChild(1).GetComponent<Text>().color = Color.black;
        SkillsTextToType1.transform.GetChild(2).GetComponent<Text>().color = Color.black;
        SkillsTextToType1.transform.GetChild(3).GetComponent<Text>().color = Color.black;
        SkillsTextToType1.transform.GetChild(4).GetComponent<Text>().color = Color.black;
        SkillsTextToType1.transform.GetChild(5).GetComponent<Text>().color = Color.black;
        SkillsTextToType1.transform.GetChild(6).GetComponent<Text>().color = Color.black;
        SkillsTextToType1.transform.GetChild(7).GetComponent<Text>().color = Color.black;
    }

    IEnumerator DipatuanModeOff()
    {
        yield return new WaitForSeconds(8.9f);

        damageEnemyScript.isDipatuanModeActivated = false;

        fireOnSword1.SetActive(false);
        fireOnSword2.SetActive(false);

        fireTurnOff = true;
    }

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(1.3f);
        charControl.enabled = true;
    }

}
