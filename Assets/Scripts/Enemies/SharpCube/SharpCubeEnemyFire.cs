using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharpCubeEnemyFire : MonoBehaviour 
{
    public GameObject bolt;
    public float fireRate;

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
        Instantiate(bolt, trans.position, trans.rotation);
    }
}