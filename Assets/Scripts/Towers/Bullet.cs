using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 0;

    public void setDamage(float newDamages)
    {
        damage = newDamages;
    }
    public float GetBulletDamage()
    {
        return damage;
    }
    
}
