using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragWindow : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform window;
    private Canvas canvas;
    private RectTransform canvasRect;

    void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasRect = canvas.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Move to front
        window.SetAsLastSibling();
        
        // Move window
        window.anchoredPosition += eventData.delta / canvas.scaleFactor;
        
        // Keep window within canvas bounds
        ClampWindowToCanvas();
    }

    private void ClampWindowToCanvas()
    {
        Vector2 canvasSize = canvasRect.rect.size;
        Vector2 windowSize = window.rect.size;

        // Calculate boundaries
        float minX = (windowSize.x - canvasSize.x) * 0.5f;
        float maxX = (canvasSize.x - windowSize.x) * 0.5f;
        float minY = (windowSize.y - canvasSize.y) * 0.5f;
        float maxY = (canvasSize.y - windowSize.y) * 0.5f;

        // Get current position
        Vector2 pos = window.anchoredPosition;

        // Clamp position
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        // Apply clamped position
        window.anchoredPosition = pos;
    }
}
