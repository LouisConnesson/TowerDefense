using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 0;
    [SerializeField]
    private float waitTime = 5f;
    private void Start()
    {
        StartCoroutine("DestroyBullet");
    }
    public void setDamage(float newDamages)
    {
        damage = newDamages;
    }
    public float GetBulletDamage()
    {
        return damage;
    }
    private IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(waitTime);
        Debug.Log("Destruction");
        Destroy(gameObject);
    }
    
}
