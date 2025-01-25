using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [Tooltip("The Window to close.")]
    public GameObject panel;
    
    public void OnClick()
    {
        panel.SetActive(false);
    }
}
