using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BraveryMode : MonoBehaviour
{
    Image skillColorYellow;

    private bool onCooldown;

    public bool isBraveryModeActivated;

    Animator anim;

    GameObject forceField;

    MainCharacterController charControl;

    //text that will pop up to type
    GameObject SkillsTextToType3;

    private int correctedLetters = 0;

    private bool typingMode = false;


    private string remainingWord = string.Empty;
    private string currentWord = "juramentado";

    private void Awake()
    {
        anim = gameObject.GetComponent<Animator>();

        charControl = gameObject.GetComponent<MainCharacterController>();

        isBraveryModeActivated = false;
        skillColorYellow = GameObject.FindGameObjectWithTag("BraverySkillYellowImage").GetComponent<Image>();
        forceField = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).gameObject;
        onCooldown = false;

        //text that will pop up to type
        SkillsTextToType3 = GameObject.FindGameObjectWithTag("SkillsTextToType3");

        //text that will pop up to type show hide
        SkillsTextToType3.SetActive(false);

        SetCurrentWord();
    }

    // Update is called once per frame
    void Update()
    {
        if (typingMode)
        {
            gameObject.GetComponent<DipatuanMode>().enabled = false;
            gameObject.GetComponent<SlowMotionMode>().enabled = false;
            if (Input.anyKeyDown)
            {
                string keyPressed = Input.inputString;


                if (keyPressed.Length == 1)
                    EnterLetter(keyPressed);
            }
        }
        else
        {
            gameObject.GetComponent<DipatuanMode>().enabled = true;
            gameObject.GetComponent<SlowMotionMode>().enabled = true;
        }

        if (Input.GetButtonDown("BraveryMode") && onCooldown == false)
        {
            setLetterColorToBlack();
            StartCoroutine("StopTypingMode");
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.2f;
            }
            //Sets typing mode to true
            typingMode = true;

            //show text that will pop up to type 
            SkillsTextToType3.SetActive(true);

            StartCoroutine("StopTypingMode");
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
        forceField.SetActive(false);
        isBraveryModeActivated = false;

    }

    IEnumerator moveAgain()
    {
        yield return new WaitForSeconds(1f);
        charControl.enabled = true;

    }

    private void SetCurrentWord()
    {
        remainingWord = currentWord;
    }

    private void EnterLetter(string typedLetter)
    {
        if (IsCorrectLetter(typedLetter))
        {
            SkillsTextToType3.transform.GetChild(correctedLetters).GetComponent<Text>().color = Color.green;
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
        yield return new WaitForSeconds(0.5f);

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
        SkillsTextToType3.SetActive(false);
    }

    IEnumerator StopTypingMode()
    {
        yield return new WaitForSeconds(0.5f);

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
        SkillsTextToType3.SetActive(false);
    }


    private void ExecuteSkill()
    {
        isBraveryModeActivated = true;

        skillColorYellow.fillAmount = 0;
        onCooldown = true;

        //Activate animation for bravery mode
        anim.SetTrigger("braveryMode");

        //Apply ForceField
        forceField.SetActive(true);

        //Stops ACtion of dipatuan when performing animation
        charControl.enabled = false;

        //Allow Dipatuan to move again
        StartCoroutine("moveAgain");

        //turn off bravery mode
        StartCoroutine("braveryModeTurnOff");


        //Sets typing mode to false
        typingMode = false;

        //hides text that will pop up to type 
        SkillsTextToType3.SetActive(false);
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
        SkillsTextToType3.transform.GetChild(0).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(1).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(2).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(3).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(4).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(5).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(6).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(7).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(8).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(9).GetComponent<Text>().color = Color.red;
        SkillsTextToType3.transform.GetChild(10).GetComponent<Text>().color = Color.red;
    }

    private void setLetterColorToBlack()
    {
        SkillsTextToType3.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(1).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(2).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(3).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(4).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(5).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(6).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(7).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(8).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(9).GetComponent<Text>().color = Color.black;
        SkillsTextToType3.transform.GetChild(10).GetComponent<Text>().color = Color.black;
    }
}
