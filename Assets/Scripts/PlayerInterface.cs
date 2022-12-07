using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInterface : MonoBehaviour
{
    public int Coins = 1000;
    public GameObject textCoin;
    public GameObject textTimerSkill;
    public GameObject buttonSkill;
    public GameObject buttonSkillUsed;
    private int timer = 30;
    private void Awake()
    {
        StartCoroutine(timerCoins());
    }

    public void skillUsed()
    {
        StartCoroutine(timerSkill());
    }

    public void spawnMob1()
    {
        PlayerPrefs.SetInt("typeOfMob", 0);
        PlayerPrefs.SetInt("costOfMob", 50);
    }
    public void spawnMob2()
    {
        PlayerPrefs.SetInt("typeOfMob", 1);
        PlayerPrefs.SetInt("costOfMob", 100);
    }
    public void spawnMob3()
    {
        PlayerPrefs.SetInt("typeOfMob", 2);
        PlayerPrefs.SetInt("costOfMob", 120);
    }
    public void spawnMob4()
    {
        PlayerPrefs.SetInt("typeOfMob", 3);
        PlayerPrefs.SetInt("costOfMob", 350);
    }
    public void spawnMob5()
    {
        PlayerPrefs.SetInt("typeOfMob", 4);
        PlayerPrefs.SetInt("costOfMob", 150);
    }
    private void Update()
    {
        textCoin.GetComponent<TMPro.TextMeshProUGUI>().text = Coins.ToString();
        textTimerSkill.GetComponent<TMPro.TextMeshProUGUI>().text = timer.ToString();
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    IEnumerator timerCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            Coins++;
        }
    }
    IEnumerator timerSkill()
    {
        while (timer > 0)
        {
            yield return new WaitForSeconds(1);
            timer--;
        }
        buttonSkill.SetActive(true);
        buttonSkillUsed.SetActive(false);
        timer = 30;
    }
}
