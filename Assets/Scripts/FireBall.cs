using Unity.Netcode;
using UnityEngine;

public class FireBall : NetworkBehaviour
{
    [SerializeField] private float damages = 20;
    [SerializeField] private ParticleSystem fireBallParticles;
    [SerializeField] private ParticleSystem explosionParticles;
    public Collider explosionCollider;
    void Start()
    {
        fireBallParticles.Play();
        explosionParticles.Stop();
        if(explosionCollider)
        explosionCollider.enabled = false;


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Toggle the Collider on and off when pressing the space bar
            explosionCollider.enabled = !explosionCollider.enabled;

            //Output to console whether the Collider is on or not
            Debug.Log("Collider.enabled = " + explosionCollider.enabled);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (!IsOwner) return;
        fireBallParticles.Stop();
        if (explosionCollider)
            explosionCollider.enabled = true;

        explosionParticles.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "mob")
        {
            Debug.Log("mob detected");

            other.gameObject.GetComponent<MobLife>().TakeDamage(damages);
        }
    }
}
