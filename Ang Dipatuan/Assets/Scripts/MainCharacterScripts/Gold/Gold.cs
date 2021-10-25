using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gold : MonoBehaviour
{
    Text gold;

    public int total_gold;

    private void Awake()
    {
        gold = GameObject.FindGameObjectWithTag("Gold").GetComponent<Text>();

        total_gold = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gold.text = total_gold.ToString();

        if(total_gold < 0)
        {
            total_gold = 0;
        }
    }

    public void addGold(int addValue)
    {
        total_gold += addValue;
    }

    public void decreaseGold(int decreaseValue)
    {
        if(total_gold > 0)
        {
            total_gold -= decreaseValue;
        }
    }
}
