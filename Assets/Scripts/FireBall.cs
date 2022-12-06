using Unity.Netcode;
using UnityEngine;

public class FireBall : NetworkBehaviour
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
        if (!IsOwner) return;
        fireBallParticles.Stop();
        explosionParticles.Play();
    }
}
