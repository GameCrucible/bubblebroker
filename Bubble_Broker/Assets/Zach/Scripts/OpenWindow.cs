using UnityEngine;

public class OpenWindow : MonoBehaviour
{
    [Tooltip("The Window to open.")]
    public GameObject panel;
    [Tooltip("Maximum clicks before guaranteed open")]
    public int maxClickCount = 10;
    
    private int currentClickCount;

    public void OnClick()
    {
        currentClickCount++;
        
        // Check if we should open (1/3 chance or max clicks reached)
        if (currentClickCount < maxClickCount && Random.Range(0, 3) != 0) 
            return;

        // Reset counter
        currentClickCount = 0;
        
        // Set to center of screen
        panel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        
        // Set to front
        panel.transform.SetAsLastSibling();
        
        panel.SetActive(true);
    }
}
