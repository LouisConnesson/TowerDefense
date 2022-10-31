using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class skills
{
    public List<GameObject> List;
}
public class CanvasController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject classesMenu;
    [SerializeField] private List<skills> canvasMenu = new List<skills>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Trigger(int skill)
    {
        if (skill == 0)
        {
            mainMenu.SetActive(false);
            classesMenu.SetActive(false);
        }
        else
        {
            int classe = GetComponent<ConstructManager>().GetClass();
            Debug.Log("classe :" + classe + " skill: " + skill);
            mainMenu.SetActive(false);
            canvasMenu[classe].List[skill-1].SetActive(true);
        }
       
    }
    private void SetClass(int newClass)
    {
        GetComponent<ConstructManager>().SetClass(newClass);
    }
    public void Exit()
    {
        mainMenu.SetActive(true);
        /*for(int i = 0; i < canvasMenu.Count; i++)
        {
            for (int j = 0; j < canvasMenu[i].List.Count; j++)
                canvasMenu[i].List[j].SetActive(false);
        }*/
    }
}
