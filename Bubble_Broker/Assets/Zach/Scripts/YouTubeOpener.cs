using UnityEngine;
using System.Collections;

public class YouTubeOpener : MonoBehaviour
{
    [Header("YouTube Settings")]
    [Tooltip("Full YouTube URL including https://")]
    public string youtubeURL = "https://www.youtube.com/watch?v=xvFZjo5PgG0";

    public void OpenYouTube()
    {
        // Always start with https URL
        string formattedURL = youtubeURL.Contains("https://") ? youtubeURL : "https://" + youtubeURL;

        #if UNITY_ANDROID || UNITY_IOS
                StartCoroutine(TryOpenNativeApp(formattedURL));
        #else
                Application.OpenURL(formattedURL);
        #endif
    }

    private IEnumerator TryOpenNativeApp(string url)
    {
        // Try to open in native app first
        string appURL = url.Replace("https://", "youtube://");
        Application.OpenURL(appURL);

        // Wait a moment to check if the native app opened
        yield return new WaitForSecondsRealtime(0.5f);

        // If we're still in the app, open the browser fallback
        Application.OpenURL(url);
    }
}
