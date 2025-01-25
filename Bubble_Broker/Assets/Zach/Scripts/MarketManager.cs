using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MarketManager : MonoBehaviour
{
    [NonSerialized]
    public int risk = 0;
    
    // [Tooltip("Time in seconds for a day to pass.")]
    // public int dayLength = 5;
    
    [Tooltip("In game days for a quarter to pass.")]
    public int fiscalQuarterLength = 120;
    
    public int currentQuarter = 1;
    
    public int currentDay = 1;
    
    [Header("Text References")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI quarterText;
    public TextMeshProUGUI riskText;

    public void Start()
    {
        StartCoroutine(UpdateDay());
    }

    private IEnumerator UpdateDay()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);  //Time range for days
            currentDay++;
            risk += Random.Range(1, 3);
        
            if (currentDay >= fiscalQuarterLength)
            {
                currentDay = 1;
                currentQuarter++;
                risk -= Random.Range(10, 15);
                
                fiscalQuarterLength = Random.Range(120, 180);
            }
        
            dayText.text = "Day: " + currentDay;
            quarterText.text = "Quarter: " + currentQuarter;
            riskText.text = "Risk (%): " + risk;
        }
    }
}
