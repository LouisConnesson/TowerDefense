using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSkill : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private ParticleSystem magicBallParticles;
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private float lifeDuration;

    //[SerializeField] private ParticleSystem explosionParticles;
    void Start()
    {
        Debug.Log("Apparition magic ball");
        magicBallParticles.Play();
        explosionParticles.SetActive(false);
        explosionParticles.GetComponent<ParticleSystem>().Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator DestroyWall()
    {
        yield return new WaitForSeconds(lifeDuration);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        magicBallParticles.Stop();
        explosionParticles.SetActive(true);
        explosionParticles.GetComponent<ParticleSystem>().Play();

        StartCoroutine("DestroyWall");
    }
}
