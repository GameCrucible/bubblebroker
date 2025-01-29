using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class BackgroundSwitcher : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("List of background sprites to cycle through")]
    public List<Sprite> backgrounds;
    
    [Tooltip("Time to display each image before transitioning")]
    public float timePerImage = 5f;
    
    [Tooltip("Duration of the crossfade transition between images")]
    public float transitionTime = 1f;

    private Image currentImage;
    private Image nextImage;
    private int currentIndex = 0;

    private void Awake()
    {
        currentImage = GetComponent<Image>();
        InitializeNextImage();
        
        if (backgrounds.Count > 0)
        {
            currentImage.sprite = backgrounds[0];
            currentImage.color = Color.white;
        }
    }

    private void Start()
    {
        if (backgrounds.Count > 1)
        {
            StartCoroutine(BackgroundCycle());
        }
    }

    private void InitializeNextImage()
    {
        // Create secondary image for transitions
        GameObject nextImageObj = new GameObject("NextImage");
        nextImageObj.transform.SetParent(transform, false);
        
        nextImage = nextImageObj.AddComponent<Image>();
        RectTransform rt = nextImageObj.GetComponent<RectTransform>();
        
        // Full-screen alignment
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        
        nextImage.color = new Color(1, 1, 1, 0);
    }

    private IEnumerator BackgroundCycle()
    {
        while (true)
        {
            // Wait before starting transition
            yield return new WaitForSeconds(timePerImage);

            // Get next background index
            int nextIndex = (currentIndex + 1) % backgrounds.Count;
            
            // Perform transition
            yield return StartCoroutine(CrossfadeTransition(
                backgrounds[nextIndex], 
                transitionTime
            ));
            
            currentIndex = nextIndex;
        }
    }

    private IEnumerator CrossfadeTransition(Sprite newSprite, float duration)
    {
        if (duration <= 0)
        {
            // Instant transition if duration is 0
            currentImage.sprite = newSprite;
            yield break;
        }

        // Set up next image
        nextImage.sprite = newSprite;
        float timer = 0f;

        Color currentColor = currentImage.color;
        Color nextColor = nextImage.color;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);

            // Fade out current image
            currentImage.color = new Color(
                currentColor.r, 
                currentColor.g, 
                currentColor.b, 
                1 - t
            );

            // Fade in next image
            nextImage.color = new Color(
                nextColor.r,
                nextColor.g,
                nextColor.b,
                t
            );

            yield return null;
        }

        // Transition complete - swap references
        currentImage.sprite = newSprite;
        currentImage.color = Color.white;
        nextImage.color = new Color(1, 1, 1, 0);
    }
}
