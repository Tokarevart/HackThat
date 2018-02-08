using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class BlackCubeCollision : MonoBehaviour 
{
    public HP startHealth;
    public Damage playerBoltDamage;
    public float takeDamageAnimDuration;

    private int health;
    private AudioSource soundEffectSource;
    private Renderer meshRen;
    private bool playAnimation = false;
    private float dAlpha;
    private Color startColor;

    void Start()
    {
        health = startHealth.Value;
        if (health < 1)
            Destroy(gameObject);

        soundEffectSource = GameObject.FindGameObjectWithTag("ObjDestructionSound").GetComponent<AudioSource>();
        meshRen = GetComponentsInChildren<Renderer>()[1];
        startColor = meshRen.material.GetColor("_TintColor");

        dAlpha = 1f / takeDamageAnimDuration;
    }

    void Update()
    {
        if (playAnimation)
            DoTakeDamageAnimationIteration();
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

        StartTakeDamageAnimation();
    }

    void StartTakeDamageAnimation()
    {
        if (playAnimation)
            StopTakeDamageAnimation();
         
        playAnimation = true;
    }

    void DoTakeDamageAnimationIteration()
    {
        Color bufColor = meshRen.material.GetColor("_TintColor");

        meshRen.material.SetColor("_TintColor", 
            new Color(startColor.r, startColor.g, startColor.b, 
                Mathf.MoveTowards(bufColor.a, 1f, dAlpha * Time.deltaTime)));

        if (Mathf.Approximately(bufColor.a, 1f))
            StopTakeDamageAnimation();
    }

    void StopTakeDamageAnimation()
    {
        meshRen.material.SetColor("_TintColor", 
            new Color(startColor.r, startColor.g, startColor.b, 0f));
        
        playAnimation = false;
    }
}