using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Data;
using UnityEngine.Rendering.Universal;

public class CallingSystem : MonoBehaviour
{
    private float timeUntilNextCall = 15f;

    private float timer;
    private float callerTimer;

    public Dialogue dialogueSystem;

    private int queue;

    public Animator animator;

    public CallerPatience callerPatience;

    public AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Debug.Log("Investor Calling");
            callerPatience.IncreaseQueue();
            ResetTimer();
        }
    }

    public void PickUpCall()
    {
        if (callerPatience.GetQueue() > 0)
        {
            audio.Play();
            animator.SetBool("PhoneIn", true);
            dialogueSystem.PickInvestor();
            callerPatience.CallPickedUp();
        }
    }

    public void ResetTimer()
    {
        timeUntilNextCall -= 0.2f;
        timeUntilNextCall = Mathf.Clamp(timeUntilNextCall, 0.5f, 100f);
        timer = Random.Range(timeUntilNextCall - 5f, timeUntilNextCall + 5f);
    }

}
