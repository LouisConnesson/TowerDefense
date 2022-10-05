using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public void spawnMob1()
    {
        PlayerPrefs.SetInt("typeOfMob", 0);
    }
    public void spawnMob2()
    {
        PlayerPrefs.SetInt("typeOfMob", 1);
    }
}
