using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MarketManager : MonoBehaviour
{
    [Header("Text References")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI quarterText;
    public TextMeshProUGUI riskText;
    public TextMeshProUGUI moneyText;

    public void Start()
    {
        StartCoroutine(UpdateDay());
    }

    private IEnumerator UpdateDay()
    {
        while (true)
        {
            yield return new WaitForSeconds(GameManager.instance.dayTick);
            
            dayText.text = "Day: " + GameManager.instance.currentDay;
            quarterText.text = "Quarter: " + GameManager.instance.currentQuarter;
            riskText.text = "Market Instability: " + GameManager.instance.risk + "%";
            moneyText.text = "Portfolio Value: " + GameManager.instance.money.ToString("C0");
        }
    }
}
