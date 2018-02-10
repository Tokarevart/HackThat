using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMover : MonoBehaviour 
{
    public float moveSpeed;
    public float rotationSpeed;

    private float velocityMagn;
    private float radRotationSpeed;
    private Rigidbody enemyRb;
    private Transform playerTransform;
    private Transform enemyTransform;
    private Vector3 enemyToPlayer;
    private Vector3 bufVel = new Vector3(1, 1, 1);

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyRb = GetComponent<Rigidbody>();
        enemyTransform = GetComponent<Transform>();

        radRotationSpeed = rotationSpeed * Mathf.PI / 180f;

        enemyToPlayer = playerTransform.position - enemyTransform.position;
        enemyToPlayer.y = 0f;
        enemyToPlayer.Normalize();
        enemyRb.velocity = enemyToPlayer * moveSpeed;

        velocityMagn = enemyRb.velocity.magnitude;
	}
	
	void FixedUpdate() 
    {
        enemyToPlayer = playerTransform.position - enemyTransform.position;
        enemyToPlayer.y = 0f;
        enemyToPlayer.Normalize();

        enemyRb.velocity = Vector3.RotateTowards(enemyRb.velocity, enemyToPlayer, radRotationSpeed * Time.fixedDeltaTime, 0f); // normalized проверить
        if (!Mathf.Approximately(enemyRb.velocity.magnitude, velocityMagn))
        {
            bufVel = enemyRb.velocity;
            bufVel.y = 0f;
            enemyRb.velocity = bufVel.normalized * moveSpeed;
        }
        enemyRb.rotation = Quaternion.LookRotation(enemyRb.velocity);
	}
}
