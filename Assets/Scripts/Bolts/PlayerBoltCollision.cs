using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoltCollision : MonoBehaviour 
{	
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player" && 
            other.gameObject.tag != "PlayerBolt")
            Destroy(gameObject);
	}
}