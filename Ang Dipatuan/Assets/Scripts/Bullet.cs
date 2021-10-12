using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;

    GameObject spawnPoint;
    private float speed = 5000f;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform.GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BulletSpawner()
    {
        GameObject instBullet = Instantiate(bullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;

        Rigidbody instBulletRigidBody = instBullet.GetComponent<Rigidbody>();

        instBulletRigidBody.AddForce(transform.forward * speed);
    }
}
