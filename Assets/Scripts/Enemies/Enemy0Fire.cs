using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy0Fire : MonoBehaviour 
{
    public GameObject bolt;
    public float fireRate;

    private float nextFireTime;
    private float fireDeltaTime;

	void Start() 
    {
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
        Instantiate(bolt, transform.position, transform.rotation);
    }
}