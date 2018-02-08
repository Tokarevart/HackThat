using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCubeCollision : MonoBehaviour 
{
    public HP startHealth;
    public Damage playerBoltDamage;

    private int health;
    private AudioSource soundEffectSource;

    void Start()
    {
        health = startHealth.Value;
        if (health < 1)
            Destroy(gameObject);

        soundEffectSource = GameObject.FindGameObjectWithTag("ObjDestructionSound").GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerBolt")
            TakeDamage(playerBoltDamage.Value);
    }

    void TakeDamage(int damageValue)
    {
        health -= damageValue;

        if (health < 1)
        {
            soundEffectSource.Play();
            Destroy(gameObject);
        }
    }
}