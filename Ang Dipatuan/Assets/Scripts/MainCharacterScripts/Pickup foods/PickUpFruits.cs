using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpFruits : MonoBehaviour
{
    GameObject item;
    private bool canPickUp;


    private void Awake()
    {
        canPickUp = false;
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
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Avocado"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(true);

            item = other.gameObject;
            canPickUp = true;
        }

        if (other.CompareTag("Coconut"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(true);

            item = other.gameObject;
            canPickUp = true;
        }

        if (other.CompareTag("FruitBasket"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(true);

            item = other.gameObject;
            canPickUp = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Avocado"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(false);

            item = other.gameObject;
            canPickUp = false;
        }

        if (other.CompareTag("Coconut"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(false);

            item = other.gameObject;
            canPickUp = false;
        }

        if (other.CompareTag("FruitBasket"))
        {
            other.transform.Find("Canvas").gameObject.SetActive(false);

            item = other.gameObject;
            canPickUp = false;
        }
    }

    IEnumerator DestroyPickedUp()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(item.gameObject);
        gameObject.GetComponent<MainCharacterController>().enabled = true;
    }
}
