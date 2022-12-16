using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{

    public ParticleSystem particle;
    public float timeBeforeClear = 2f;
    private bool inside = false;
    private float timer;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (particle == null)
        {
            particle = GetComponentInChildren<ParticleSystem>();
        }
        if (other.CompareTag("Player"))
        {
            particle.Play();
            inside = true;
            timer = timeBeforeClear;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inside = false;
        }
    }
    private void Update()
    {
        if (!inside)
        {
            timer -= Time.deltaTime;
        
            if(timer <= 0)
            {
                particle.Stop();
                particle.Clear();
            }
        }
    }
}
