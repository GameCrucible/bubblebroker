using UnityEngine;

public class TurnOffComputer : MonoBehaviour
{
    public GameObject canvasObject;

    public GameObject logOutPanel;

    public GameObject bootPanel;
    
    public ProgressBar progressBar;
    
    public void TurnOff()
    {
        logOutPanel.SetActive(false);
        bootPanel.SetActive(true);
        canvasObject.SetActive(false);
    }
}
