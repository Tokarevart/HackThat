using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleSphereCollision : MonoBehaviour 
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "OrangeSphere" && 
            other.gameObject.tag != "PurpleSphere" &&
            other.gameObject.tag != "PlayerBolt")
            Destroy(gameObject);
    }
}