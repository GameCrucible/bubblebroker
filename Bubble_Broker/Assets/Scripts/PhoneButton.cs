using UnityEngine;
using UnityEngine.UI;

public class PhoneButton : MonoBehaviour
{
    public GameObject canvasObject;
    public GameObject otherObject;
    
    public void TurnOnCanvas()
    {
        canvasObject.SetActive(true);
    }
    public void TurnOffCanvas()
    {
        canvasObject?.SetActive(false);
    }
    public void TurnOnObject()
    {
        otherObject.SetActive(true);
    }
}
