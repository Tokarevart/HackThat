using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMover : MonoBehaviour 
{
    public float speed;
    public float flyDistance;

    private Vector3 startPosition;
    private Vector3 movement;

    void Start()
    {
        startPosition = transform.position;

        movement = TAUnityLib.RotateVector3(Vector3.forward, transform.rotation);
    }
	
	void FixedUpdate() 
    {
        transform.position += movement * speed * Time.fixedDeltaTime;

        if ((transform.position - startPosition).magnitude > flyDistance)
            Destroy(gameObject);
	}
}