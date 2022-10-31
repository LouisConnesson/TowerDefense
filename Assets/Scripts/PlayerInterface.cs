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
    public void spawnMob3()
    {
        PlayerPrefs.SetInt("typeOfMob", 2);
    }
    public void spawnMob4()
    {
        PlayerPrefs.SetInt("typeOfMob", 3);
    }
    public void spawnMob5()
    {
        PlayerPrefs.SetInt("typeOfMob", 4);
    }
}
