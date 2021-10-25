using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClotheinInventory : MonoBehaviour
{
    GameObject defaultClothe, Clothe1, Clothe2, Clothe3;

    public bool boughtCloth1, boughtCloth2, boughtCloth3;

    private void Awake()
    {
        defaultClothe = GameObject.FindGameObjectWithTag("DefaultCloth");
        Clothe1 = GameObject.FindGameObjectWithTag("Cloth1");
        Clothe2 = GameObject.FindGameObjectWithTag("Cloth2");
        Clothe3 = GameObject.FindGameObjectWithTag("Cloth3");

        boughtCloth1 = false;
        boughtCloth2 = false;
        boughtCloth3 = false;
    }


    // Update is called once per frame
    void Update()
    {

        if(boughtCloth1 == true)
        {
            Clothe1.SetActive(true);
        }
        else
        {
            Clothe1.SetActive(false);
        }

        if (boughtCloth2 == true)
        {
            Clothe2.SetActive(true);
        }
        else
        {
            Clothe2.SetActive(false);
        }

        if (boughtCloth3 == true)
        {
            Clothe3.SetActive(true);
        }
        else
        {
            Clothe3.SetActive(false);
        }



    }

  
}
