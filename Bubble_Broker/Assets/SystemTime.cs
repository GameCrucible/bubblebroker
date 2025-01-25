using TMPro;
using UnityEngine;

public class SystemTime : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //Set the timeText to the current system time (12 hr format)
        timeText.text = System.DateTime.Now.ToString("h:mm tt");
    }
}
