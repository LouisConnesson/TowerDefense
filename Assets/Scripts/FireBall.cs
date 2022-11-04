using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem fireBallParticles;
    [SerializeField] private ParticleSystem explosionParticles;
    void Start()
    {
        fireBallParticles.Play();
        explosionParticles.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        fireBallParticles.Stop();
        explosionParticles.Play();
    }
}
