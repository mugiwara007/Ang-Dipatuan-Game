using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvocadoSpawner : MonoBehaviour
{
    public GameObject avocadoGameObject;

    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        var avocado = Instantiate(avocadoGameObject, gameObject.transform.position, gameObject.transform.rotation);
        avocado.transform.parent = gameObject.transform;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.childCount == 0)
        {
            timer += Time.deltaTime;

            if (timer >= 100f)
            {
                var avocado = Instantiate(avocadoGameObject, gameObject.transform.position, gameObject.transform.rotation);
                avocado.transform.parent = gameObject.transform;
                timer = 0f;
            }
        }
    }
}
