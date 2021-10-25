using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothe2 : MonoBehaviour
{
    PlayerBar playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBar>();
    }

    public void EquipClothe()
    {
        playerStats.maxHealth = 150;
    }
}
