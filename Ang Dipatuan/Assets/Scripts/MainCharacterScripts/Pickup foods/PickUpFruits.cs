using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpFruits : MonoBehaviour
{
    GameObject item;
    private bool canPickUp;

    Inventory fruit;

    private string fruit_picked_up;

    Text InventoryFullText;

    private void Awake()
    {
        canPickUp = false;

    }

    private void Start()
    {
        InventoryFullText = GameObject.FindGameObjectWithTag("InventoryFullText").GetComponent<Text>();
        InventoryFullText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canPickUp)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                gameObject.GetComponent<Animator>().SetTrigger("pickup");
                gameObject.GetComponent<MainCharacterController>().enabled = false;
                StartCoroutine("DestroyPickedUp");
                canPickUp = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avocado"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(true);

 
            fruit_picked_up = "avocado";
            item = other.gameObject;
            canPickUp = true;
        }

        if (other.CompareTag("Coconut"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(true);
               
          
            fruit_picked_up = "coconut";
            item = other.gameObject;
            canPickUp = true;
        }

        if (other.CompareTag("FruitBasket"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(true);

           
            fruit_picked_up = "fruitbasket";
            item = other.gameObject;
            canPickUp = true;
        }

        if (other.CompareTag("Pineapple"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(true);

            fruit_picked_up = "pineapple";
            item = other.gameObject;
            canPickUp = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Avocado"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(false);
            fruit_picked_up = "";
            item = other.gameObject;
            canPickUp = false;
        }

        if (other.CompareTag("Coconut"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(false);
            fruit_picked_up = "";
            item = other.gameObject;
            canPickUp = false;
        }

        if (other.CompareTag("FruitBasket"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(false);
            fruit_picked_up = "";
            item = other.gameObject;
            canPickUp = false;
        }

        if (other.CompareTag("Pineapple"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(false);
            fruit_picked_up = "";
            item = other.gameObject;
            canPickUp = false;
        }

        
    }

    IEnumerator DestroyPickedUp()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.GetComponent<MainCharacterController>().enabled = true;

        if (fruit_picked_up == "avocado")
        {
            if (gameObject.GetComponent<Inventory>().avocado < 5)
            {
                gameObject.GetComponent<Inventory>().avocado += 1;
            }
            else
            {
                InventoryFullText.enabled = true;
                StartCoroutine("DisableInventoryFullText");
                yield break;
            }
        }

        if (fruit_picked_up == "coconut")
        {
            if (gameObject.GetComponent<Inventory>().coconut < 5)
            {
                gameObject.GetComponent<Inventory>().coconut += 1;
            }
            else
            {
                InventoryFullText.enabled = true;
                StartCoroutine("DisableInventoryFullText");
                yield break;
            }
        }

        if (fruit_picked_up == "fruitbasket")
        {
            if (gameObject.GetComponent<Inventory>().fruitbasket < 5)
            {
                gameObject.GetComponent<Inventory>().fruitbasket += 1;
            }
            else
            {
                InventoryFullText.enabled = true;
                StartCoroutine("DisableInventoryFullText");
                yield break;
            }
        }

        if (fruit_picked_up == "pineapple")
        {
            if (gameObject.GetComponent<Inventory>().pineapple < 5)
            {
                gameObject.GetComponent<Inventory>().pineapple += 1;
            }
            else
            {
                InventoryFullText.enabled = true;
                StartCoroutine("DisableInventoryFullText");
                yield break;
            }
        }

        Destroy(item.gameObject);
        fruit_picked_up = "";
    }

    IEnumerator DisableInventoryFullText()
    {
        yield return new WaitForSeconds(1.2f);
        InventoryFullText.enabled = false;
    }

    }
