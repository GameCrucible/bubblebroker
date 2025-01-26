using UnityEngine;

public class TurnOnPC : MonoBehaviour
{
    public GameObject canvasObject;
    
    public ProgressBar progressBar;
    
    public void TurnOn()
    {
        canvasObject.SetActive(true);
        progressBar.rerun();
    }
}
