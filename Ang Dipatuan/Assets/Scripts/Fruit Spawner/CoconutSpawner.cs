using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutSpawner : MonoBehaviour
{

    public GameObject coconutGameObject;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.childCount == 0)
        {
            timer += Time.deltaTime;

            if(timer >= 100f)
            {
                var coconut = Instantiate(coconutGameObject, gameObject.transform.position, gameObject.transform.rotation);
                coconut.transform.parent = gameObject.transform;
                timer = 0f;
            }
        }
    }

}
