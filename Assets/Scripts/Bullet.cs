using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage = 2f;

    public float GetBulletDamage()
    {
        return damage;
    }
    
}
