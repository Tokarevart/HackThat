using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeSphereCollision : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "OrangeSphere" && 
            other.gameObject.tag != "PurpleSphere" &&
            other.gameObject.tag != "Enemy0")
            Destroy(gameObject);
    }
}