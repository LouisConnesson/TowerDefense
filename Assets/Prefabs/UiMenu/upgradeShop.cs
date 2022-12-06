using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgradeShop : MonoBehaviour
{
    public int nb = 0;
    public GameObject textSkill;
    public PlayerInterface m_PlayerInterface;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Upgrade()
    {
        if (m_PlayerInterface.Coins - 50 >= 0)
        {
            nb++;
            textSkill.GetComponent<TMPro.TextMeshProUGUI>().text = nb.ToString();
            m_PlayerInterface.Coins = m_PlayerInterface.Coins - 50;
        }
        else
            Debug.Log("Not Enough Money !");
    }
}
