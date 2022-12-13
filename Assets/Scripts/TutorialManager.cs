using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public int tutorialIndex;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private string[] TutorialText = { 

    };




    // Start is called before the first frame update
    void Start()
    {
        tutorialIndex = 0;
        FirstTutorialStep();
    }

 
    private void FirstTutorialStep()
    {
        tutorialText.text = TutorialText[0];

    }
    public void TutorialTextManager(bool isNext)
    {
        if (isNext && tutorialIndex < TutorialText.Length -1)
            tutorialIndex++;
        else if(!isNext && tutorialIndex > 0)
            tutorialIndex--;
    }
}
