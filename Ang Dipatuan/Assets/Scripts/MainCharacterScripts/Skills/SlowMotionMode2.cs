using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlowMotionMode2 : MonoBehaviour
{
    GameObject[] enemies;
    GameObject bwPost;
    Image skillColorYellow;

    private bool onCooldown;

    Animator anim;

    MainCharacterController charControl;

    //text that will pop up to type
    GameObject SkillsTextToType2;

    private int correctedLetters = 0;

    private bool typingMode = false;


    private string remainingWord = string.Empty;
    private string currentWord = "kahangtoran";

    PlayerBar manaStats;

    private void Awake()
    {
        skillColorYellow = GameObject.FindGameObjectWithTag("SlowMoSkillYellowImage").GetComponent<Image>();
        bwPost = GameObject.FindGameObjectWithTag("NormalPost").transform.GetChild(0).gameObject;
        onCooldown = false;

        anim = gameObject.GetComponent<Animator>();
        charControl = gameObject.GetComponent<MainCharacterController>();

        //text that will pop up to type
        SkillsTextToType2 = GameObject.FindGameObjectWithTag("SkillsTextToType2");

        //text that will pop up to type show hide
        SkillsTextToType2.SetActive(false);

        SetCurrentWord();

        manaStats = gameObject.GetComponent<PlayerBar>();
    }

    // Update is called once per frame
    void Update()
    {
        if (typingMode)
        {
            gameObject.GetComponent<DipatuanMode>().enabled = false;
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
            gameObject.GetComponent<DipatuanMode>().enabled = true;
            gameObject.GetComponent<BraveryMode>().enabled = true;
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");


        if (manaStats.mana >= 25)
        {
            if (Input.GetButtonDown("SlowMotionMode") && onCooldown == false)
            {
                manaStats.ReduceMana(25);
                setLetterColorToBlack();
                StartCoroutine("StopTypingMode");
                if (Time.timeScale == 1.0f)
                {
                    Time.timeScale = 0.2f;
                }
                //Sets typing mode to true
                typingMode = true;

                //show text that will pop up to type 
                SkillsTextToType2.SetActive(true);

                StartCoroutine("StopTypingMode");
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

    IEnumerator backToNormalSpeed()
    {
        yield return new WaitForSeconds(5f);

        bwPost.SetActive(false);

        foreach (GameObject enemy in enemies)
        {
            Animator enemyAnim = enemy.GetComponent<Animator>();

            enemyAnim.SetFloat("animationSpeed", 1f);
        }
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
            SkillsTextToType2.transform.GetChild(correctedLetters).GetComponent<Text>().color = Color.green;
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
        SkillsTextToType2.SetActive(false);
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
        SkillsTextToType2.SetActive(false);
    }


    private void ExecuteSkill()
    {
        foreach (GameObject enemy in enemies)
        {
            Animator enemyAnim = enemy.GetComponent<Animator>();

            enemyAnim.SetFloat("animationSpeed", 0.2f);
        }

        //Activate Black & White Effect
        bwPost.SetActive(true);


        //Activate animation for bravery mode
        anim.SetTrigger("slowmotionMode");

        //Stops ACtion of dipatuan when performing animation
        charControl.enabled = false;

        //Allow Dipatuan to move again
        StartCoroutine("moveAgain");

        skillColorYellow.fillAmount = 0;
        onCooldown = true;
        StartCoroutine("backToNormalSpeed");

        //Sets typing mode to false
        typingMode = false;

        //hides text that will pop up to type 
        SkillsTextToType2.SetActive(false);
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
        SkillsTextToType2.transform.GetChild(0).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(1).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(2).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(3).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(4).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(5).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(6).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(7).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(8).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(9).GetComponent<Text>().color = Color.red;
        SkillsTextToType2.transform.GetChild(10).GetComponent<Text>().color = Color.red;
    }

    private void setLetterColorToBlack()
    {
        SkillsTextToType2.transform.GetChild(0).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(1).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(2).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(3).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(4).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(5).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(6).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(7).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(8).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(9).GetComponent<Text>().color = Color.black;
        SkillsTextToType2.transform.GetChild(10).GetComponent<Text>().color = Color.black;
    }
}
