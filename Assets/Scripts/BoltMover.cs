using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltMover : MonoBehaviour 
{
    public float speed = 35f;
    public float flyDistance = 35f;

    private AudioSource blackCubeDestrSoundEffect;
    private Vector3 startPosition;
    private Vector3 movement;

    void Start()
    {
        blackCubeDestrSoundEffect = GameObject.FindGameObjectWithTag("ObjDestructionSound").GetComponent<AudioSource>();

        startPosition = transform.position;

        Quaternion quat = new Quaternion(0f, 0f, 1f, 0f);
        quat = transform.rotation * quat * Quaternion.Inverse(transform.rotation);
        movement = new Vector3(quat.x, 0, quat.z);

        GetComponent<Rigidbody>().velocity = movement * speed;
    }
	
	void FixedUpdate() 
    {
        if ((transform.position - startPosition).magnitude > flyDistance)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "WhiteCube")
            Destroy(gameObject);
        if (other.gameObject.tag == "BlackCube")
        {
            blackCubeDestrSoundEffect.Play();

            Destroy(other.gameObject);
        }

        if (other.gameObject.tag != "Player" && other.gameObject.tag != "PlayerBolt") 
            Destroy(gameObject);
    }
}