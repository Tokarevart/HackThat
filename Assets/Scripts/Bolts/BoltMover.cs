using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMover : MonoBehaviour 
{
    public float speed;
    public float flyDistance;

    private Vector3 startPosition;
    private Vector3 movement;
    private Transform trans;

    void Start()
    {
        trans = GetComponent<Transform>();

        startPosition = trans.position;

        movement = TAUnityLib.RotateVector3(Vector3.forward, trans.rotation);
    }
	
	void FixedUpdate() 
    {
        trans.position += movement * speed * Time.fixedDeltaTime;

        if ((trans.position - startPosition).magnitude > flyDistance)
            Destroy(gameObject);
	}
}