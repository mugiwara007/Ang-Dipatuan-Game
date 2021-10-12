using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollision : MonoBehaviour
{

    CapsuleCollider characterCollider;
    CapsuleCollider characterCollisionBlocker;


    private void Awake()
    {
        characterCollider = gameObject.GetComponent<CapsuleCollider>();
        characterCollisionBlocker = gameObject.transform.GetChild(3).GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(characterCollider, characterCollisionBlocker, true);
    }
}
