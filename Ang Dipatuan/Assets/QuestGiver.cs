using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public QuestUI quest;

    public PlayerBar player;

    public Text txtdesc;
    public Text txtgold;


    public void Quest1()
    {
        txtdesc.text = quest.desc;
        txtgold.text = quest.goldReward.ToString();
    }


}
