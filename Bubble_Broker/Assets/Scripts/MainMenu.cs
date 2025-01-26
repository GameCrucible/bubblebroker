using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject CanvasMain;
    public GameObject CanvasTutorial1;
    public GameObject CanvasTutorial2;
    public GameObject CanvasTutorial3;
    public GameObject NextButton;

    public int tutorialStep = 0;


    public void startGame()
    {
        CanvasMain.SetActive(false);
        CanvasTutorial1.SetActive(true);
        NextButton.SetActive(true);
    }

    public void tutorial()
    {
        if (tutorialStep == 0)
        {
            CanvasTutorial1.SetActive(false);
            CanvasTutorial2.SetActive(true);
            NextButton.SetActive(true);
            tutorialStep++;
        }
        else if (tutorialStep == 1)
        {
            CanvasTutorial2.SetActive(false);
            CanvasTutorial3.SetActive(true);
            NextButton.SetActive(true);
            tutorialStep++;
        }
        else if (tutorialStep == 2)
        {
            SceneManager.LoadScene("Office");
        }
        
    }

}
