using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProgressBar : MonoBehaviour
{
    [Header("Progress Settings")]
    [SerializeField] private Sprite[] progressFrames; // Assign your progress bar sprites in order
    [SerializeField] private float minDelay = 0.1f;
    [SerializeField] private float maxDelay = 0.5f;
    [SerializeField] private float lastFrameDelay = 3f; // Extended delay for the final frame
    [SerializeField] private GameObject panel;

    private Image progressImage;
    private int currentIndex = 0;
    
    public BubblesManager bubblesManager;

    void Awake()
    {
        currentIndex = 0;
        panel.transform.SetAsLastSibling();
        
        progressImage = GetComponent<Image>();
        if (progressImage == null)
        {
            Debug.LogError("ProgressBar component requires an Image component!");
            return;
        }

        progressImage.sprite = progressFrames[currentIndex];

        if (progressFrames.Length > 0)
        {
            StartCoroutine(AnimateProgressBar());
        }
        else
        {
            Debug.LogWarning("No progress frames assigned!");
        }
    }

    IEnumerator AnimateProgressBar()
    {
        Debug.Log("Playing");
        // Animate all frames except the last one
        while (currentIndex < progressFrames.Length - 2)
        {
            progressImage.sprite = progressFrames[currentIndex];
            currentIndex++;

            // Random delay between frames
            float randomDelay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSecondsRealtime(randomDelay);
        }

        // Handle last frame with extended delay
        if (currentIndex < progressFrames.Length)
        {
            progressImage.sprite = progressFrames[currentIndex];
            yield return new WaitForSecondsRealtime(lastFrameDelay);
            progressImage.sprite = progressFrames[currentIndex+1];
            yield return new WaitForSeconds(0.2f);
            
            bubblesManager.ShowInitialMessage();

            //Hide panel
            StopCoroutine(AnimateProgressBar());
            panel.SetActive(false);
        }
    }
    
    public void rerun()
    {
        currentIndex = 0;
        panel.SetActive(true);
        StartCoroutine(AnimateProgressBar());
    }
}
