using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgChange : MonoBehaviour
{
    DamageEnemy kampilan;

    public void LightAtkDmg()
    {
        kampilan = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();
        kampilan.LightAtkDmg();
    }

    public void HeavyAtkDmg()
    {
        kampilan = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();
        kampilan.HeavyAtkDmg();
    }

    public void JumpAtkDmg()
    {
        kampilan = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();
        kampilan.JumpAtkDmg();
    }

    public void StealthAtkDmg()
    {
        kampilan = GameObject.FindGameObjectWithTag("KampilanArmed").GetComponent<DamageEnemy>();
        kampilan.StealthAtkDmg();
    }
}
