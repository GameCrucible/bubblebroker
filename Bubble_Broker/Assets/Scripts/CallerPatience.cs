using UnityEngine;

public class CallerPatience : MonoBehaviour
{
    private int queue = 0;
    private float timer;
    private bool callPickedUp = false;
    public AudioSource audio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ResetTimer();
    }


    public void ResetTimer()
    {
        timer = 8f;
    }

    public void CallPickedUp()
    {
        callPickedUp=true;
    }

    public void IncreaseQueue()
    {
        queue++;
    }

    public int GetQueue()
    {
        return queue;
    }

    // Update is called once per frame
    void Update()
    {
        if(queue > 0)
        {
            if (!(audio.isPlaying))
            {
                audio.Play();
            }
            timer -= Time.deltaTime;
            if (callPickedUp)
            {
                audio.Stop();
                callPickedUp = false;
                queue--;
                ResetTimer();
            }
            if (timer < 0f)
            {
                queue--;
                Debug.Log("failed to pick up call");
                ResetTimer();
            }
        }
    }
}
