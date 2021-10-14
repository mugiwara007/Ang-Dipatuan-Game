using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlocking : MonoBehaviour
{

    public bool isEnemyBlocking = false;

    private void Awake()
    {
        isEnemyBlocking = false;
    }

    public void Blocking()
    {
        isEnemyBlocking = true;
    }

    public void NotBlocking()
    {
        isEnemyBlocking = false;
    }

}
