using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructorAtStart : MonoBehaviour 
{
	void Start() 
    {
        Destroy(gameObject);
	}
}