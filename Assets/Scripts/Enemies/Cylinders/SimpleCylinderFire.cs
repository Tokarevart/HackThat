using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCylinderFire : MonoBehaviour 
{
	public GameObject bolt;
    public float fireRate;
    //
    // Degrees turn per second.
    public float rotationSpeed;

    private Transform trans;
    private float nextFireTime;
    private float fireDeltaTime;

	void Start() 
    {
        trans = GetComponent<Transform>();
        
        nextFireTime = 0f;
        fireDeltaTime = 1f / fireRate;
	}
	
	void Update() 
    {
        if (Time.time < nextFireTime)
            return;

        Fire();
        nextFireTime = Time.time + fireDeltaTime;
	}

    void Fire()
    {
        Instantiate(bolt, trans.position, Quaternion.AngleAxis(rotationSpeed * Time.time, Vector3.up));
        Instantiate(bolt, trans.position, Quaternion.AngleAxis(rotationSpeed * Time.time + 90f, Vector3.up));
        Instantiate(bolt, trans.position, Quaternion.AngleAxis(rotationSpeed * Time.time + 180f, Vector3.up));
        Instantiate(bolt, trans.position, Quaternion.AngleAxis(rotationSpeed * Time.time + 270f, Vector3.up));
    }
}