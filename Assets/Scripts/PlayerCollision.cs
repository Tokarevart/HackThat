using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour 
{
    public HP startHealth;
    public Damage redCubeDamage;
    public Damage orangeSphereDamage;
    public Damage purpleSphereDamage;
    public float takeDamageDeltaTime;

    private int health;
    private AudioSource soundEffectSource;
    private GameObject rightPart;
    private GameObject leftPart;
    private float nextTakeDamage = 0f;

	void Start() 
    {
        rightPart = GameObject.FindGameObjectWithTag("1stToDestroyPlayerPart");
        leftPart = GameObject.FindGameObjectWithTag("2ndToDestroyPlayerPart");

        health = startHealth.Value;
        if (health < 1)
            gameObject.SetActive(false);
        else if (health == 1)
        {
            gameObject.SetActive(true);
            leftPart.SetActive(false);
            rightPart.SetActive(false);
        }
        else if (health == 2)
        {
            gameObject.SetActive(true);
            leftPart.SetActive(true);
            rightPart.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            leftPart.SetActive(true);
            rightPart.SetActive(true);
        }

        soundEffectSource = GameObject.FindGameObjectWithTag("PlayerTakeDamageSound").GetComponent<AudioSource>();
	}
	
    void OnTriggerEnter(Collider other) 
    {
        if (Time.time < nextTakeDamage)
            return;

        if (other.gameObject.tag == "RedCube")
            TakeDamage(redCubeDamage.Value);
            
        if (other.gameObject.tag == "OrangeSphere")
            TakeDamage(orangeSphereDamage.Value);

        if (other.gameObject.tag == "PurpleSphere")
            TakeDamage(purpleSphereDamage.Value);
	}

    void OnTriggerStay(Collider other)
    {
        OnTriggerEnter(other);
    }

    void TakeDamage(int damageValue)
    {
        health -= damageValue;
        soundEffectSource.Play();
        nextTakeDamage = Time.time + takeDamageDeltaTime;

        if (health == 2)
        {
            gameObject.SetActive(true);
            leftPart.SetActive(true);
            rightPart.SetActive(false);
        }
        else if (health == 1)
        {
            gameObject.SetActive(true);
            leftPart.SetActive(false);
            rightPart.SetActive(false);
        }
        else if (health < 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            leftPart.SetActive(true);
            rightPart.SetActive(true);
        }
    }
}