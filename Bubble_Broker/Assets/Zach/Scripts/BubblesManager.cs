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
        string message = "Hey! I'm Bubbles! Your friendly AI assistant. Keep me entertained or else...";
        
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
}
