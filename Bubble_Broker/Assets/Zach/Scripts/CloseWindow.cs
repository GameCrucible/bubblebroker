using UnityEngine;

public class CloseWindow : MonoBehaviour
{
    [Tooltip("The Window to close.")]
    public GameObject panel;
    public AudioSource clickSound;
    
    public void OnClick()
    {
        if(clickSound != null)
            clickSound.Play();
        panel.SetActive(false);
    }
}
