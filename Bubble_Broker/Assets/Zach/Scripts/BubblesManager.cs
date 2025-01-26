using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BubblesManager : MonoBehaviour
{
    [SerializeField]
    public List<string> bubbleTexts;
    public float typeSpeed = 0.05f;
    public float displayTime = 3f;
    public float talkSwitchInterval = 0.2f;

    [Header("UI References")]
    public TextMeshProUGUI text;
    public Image textBox;
    public Image bubbles;

    [Header("Resources")]
    public Sprite bubbleIdle;
    public Sprite bubbleTalk;
    public AudioSource clickSound;

    private Coroutine currentRoutine;
    private bool isTyping = false;
    private float hideTimer;

    private void Start()
    {
        StartCoroutine(AddSystemInfoMessage());
        textBox.gameObject.SetActive(false);
        bubbles.sprite = bubbleIdle;
    }

    private void Update()
    {
        if (isTyping && Time.time > hideTimer)
        {
            HideTextBox();
        }
    }

    public void OnBubblesClick()
    {
        //Randomize sound pitch
        clickSound.pitch = Random.Range(0.9f, 1.1f);
        
        clickSound.Play();
        ShowNewMessage();
    }

    private void ShowNewMessage()
    {
        
        // Cancel existing coroutines
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }

        // Reset timer
        hideTimer = Time.time + displayTime;

        // Get random message
        string message = bubbleTexts.Count > 0 
            ? bubbleTexts[Random.Range(0, bubbleTexts.Count)] 
            : "I have nothing to say!";

        // Start new routine
        currentRoutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        isTyping = true;
        textBox.gameObject.SetActive(true);
        text.text = "";

        // Start talking animation
        InvokeRepeating(nameof(SwitchTalkingSprite), 0f, talkSwitchInterval);

        foreach (char c in message)
        {
            text.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }

        // Stop talking animation
        CancelInvoke(nameof(SwitchTalkingSprite));
        bubbles.sprite = bubbleIdle;

        // Wait while visible
        yield return new WaitUntil(() => Time.time > hideTimer);
        HideTextBox();
    }

    private void SwitchTalkingSprite()
    {
        bubbles.sprite = bubbles.sprite == bubbleIdle 
            ? bubbleTalk 
            : bubbleIdle;
    }

    private void HideTextBox()
    {
        CancelInvoke(nameof(SwitchTalkingSprite));
        textBox.gameObject.SetActive(false);
        bubbles.sprite = bubbleIdle;
        isTyping = false;
    }
    
    public void ShowInitialMessage()
    {
        //Randomize sound pitch
        clickSound.pitch = Random.Range(0.9f, 1.1f);
        
        clickSound.Play();
        
        //Set message
        string message = "Hey! I'm Babaru! Your friendly AI assistant. Keep me entertained or else...";
        
        // Cancel existing coroutines
        if (currentRoutine != null)
        {
            StopCoroutine(currentRoutine);
        }
        
        // Reset timer
        hideTimer = Time.time + displayTime;
        
        // Start new routine
        currentRoutine = StartCoroutine(TypeText(message));
    }
    
    private IEnumerator AddSystemInfoMessage()
    {
        List<string> infoPieces = new List<string>();
        string ipAddress = "";
        string location = "";

        // Try to get IP address (won't work in WebGL)
        try
        {
            ipAddress = GetLocalIPAddress();
            if (!string.IsNullOrEmpty(ipAddress))
            {
                infoPieces.Add($"IP: {ipAddress}");
            }
        }
        catch
        {
            // IP collection failed
        }

        // Try to get location (requires user permission)
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            int maxWait = 10;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (Input.location.status == LocationServiceStatus.Running)
            {
                LocationInfo loc = Input.location.lastData;
                location = $"{loc.latitude:F2}, {loc.longitude:F2}";
                infoPieces.Add($"Location: {location}");
            }
            Input.location.Stop();
        }

        // Add message if we collected any info
        if (infoPieces.Count > 0)
        {
            string infoMessage = "What's This?: " + string.Join(" • ", infoPieces);
            bubbleTexts.Add(infoMessage);
        }
    }

    private string GetLocalIPAddress()
    {
        var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        return "";
    }
}
