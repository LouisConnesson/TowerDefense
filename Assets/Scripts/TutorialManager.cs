using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public int tutorialIndex;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private string[] TutorialText = { "Bienvenu dans le didacticiel afin de vous apprendre � utiliser toutes les fonctionnalit�s.", 
        "Pour la premi�re �tape, vous allez essayer de construire une tour aifn de d�tuitre l'ennemi situ� devant vous" ,
        "Pour cela, cliquez sur le bouton 'cons' situ� sur l'interface attach�e a votre main gauche puis s�lectionnez le genre de tour que vous souhaitez construire",
        "Maintenant, visez le centre de la plateforme jaune puis pressez le bouton A de votre manette droite",
        "Votre premier ennemi est maintenant vaincu ! Passons � la magie",
        "Les ennemis sont maintenant plus r�sistants, afin de les empecher d'avancer il va falloir les rallentir",
        "Pour cela dirigez vous dans 'classe' et choisissez 'constructeur'. Vous pouvez ensuite choisir votre sort dans la fen�tre 'skills', utilisez la gachette afin de le lancer"

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
