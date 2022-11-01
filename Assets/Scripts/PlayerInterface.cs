using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private int Coins = 1000;
    public GameObject textCoin;
    private void Awake()
    {
        StartCoroutine(timerCoins());
    }

    public void spawnMob1()
    {
        PlayerPrefs.SetInt("typeOfMob", 0);
        if (Coins - 50 >= 0)
            Coins = Coins - 50;
    }
    public void spawnMob2()
    {
        PlayerPrefs.SetInt("typeOfMob", 1);
        if (Coins - 100 >= 0)
            Coins = Coins - 100;
    }
    public void spawnMob3()
    {
        PlayerPrefs.SetInt("typeOfMob", 2);
        if (Coins - 120 >= 0)
            Coins = Coins - 120;
    }
    public void spawnMob4()
    {
        PlayerPrefs.SetInt("typeOfMob", 3);
        if (Coins - 350 >= 0)
            Coins = Coins - 350;
    }
    public void spawnMob5()
    {
        PlayerPrefs.SetInt("typeOfMob", 4);
        if (Coins - 150 >= 0)
            Coins = Coins - 150;
    }
    private void Update()
    {
        textCoin.GetComponent<TMPro.TextMeshProUGUI>().text = Coins.ToString();
    }
    IEnumerator timerCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Coins++;
        }
    }
}
