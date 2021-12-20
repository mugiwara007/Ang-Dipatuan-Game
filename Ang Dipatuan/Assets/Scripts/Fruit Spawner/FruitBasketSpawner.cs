using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBasketSpawner : MonoBehaviour
{
    public GameObject fruitBasketSpawner;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        var fruitbasket = Instantiate(fruitBasketSpawner, gameObject.transform.position, gameObject.transform.rotation);
        fruitbasket.transform.parent = gameObject.transform;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            timer += Time.deltaTime;

            if (timer >= 200f)
            {
                var fruitbasket = Instantiate(fruitBasketSpawner, gameObject.transform.position, gameObject.transform.rotation);
                fruitbasket.transform.parent = gameObject.transform;
                timer = 0f;
            }
        }
    }
}
