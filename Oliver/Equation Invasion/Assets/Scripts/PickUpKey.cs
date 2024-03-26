using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpKey : MonoBehaviour
{
    SpriteRenderer m_renderer;
    public ParticleSystem m_particle;

    public ParticleSystem allParticles;

    public GameObject HealthBar;

    private void Start()
    {
        m_renderer = GetComponentInParent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {


            m_renderer.enabled = false;
            m_particle.Stop();
        }allParticles.Play();
        //HealthBar.GetComponent<LifeHUD>().HealPlayer();
        //Debug.Log("Hit!");
    }

}
