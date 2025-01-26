using System;
using System.Collections;
using TMPro;
using UnityEditor.XR;
using UnityEngine;
using Random = UnityEngine.Random;

public class MarketManager : MonoBehaviour
{
    [Header("Text References")]
    public TextMeshProUGUI dayText;
    public TextMeshProUGUI quarterText;
    public TextMeshProUGUI riskText;
    public TextMeshProUGUI moneyText;

    public GameObject blueScreenPanel;

    public GameObject popupPrefab1;
    public GameObject popupPrefab2;
    public GameObject popupPrefab3;

    public GameObject bsod;
    public GameObject boot;

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
            
            // 1/150 chance of blue screen; 1/50 chance of popup
            if (Random.Range(0, 150) == 0)
            {
                blueScreenPanel.transform.SetAsLastSibling();
                blueScreenPanel.SetActive(true);
            }
            else if (Random.Range(0, Math.Max(10, 50 - (GameManager.instance.currentQuarter * 3))) == 0 && !bsod.activeSelf && !boot.activeSelf && 
                     GameManager.instance.currentDay > 30)
            {
                
                //Place randomly slightly off center, as child of canvas
                Vector2 randomPosition = new Vector2(Random.Range(-20, 20), Random.Range(-20, 20));
                var popup = RandomPopup();
                
                Debug.Log("Popup" + popup);
                
                if (popup == null) continue;
                popup.transform.localPosition = randomPosition;
                popup.transform.SetAsLastSibling();
                popup.SetActive(true);
            }
        }
    }
    
    //Choose a random popup that is not already active, if all are active, return null;
    private GameObject RandomPopup()
    {
        GameObject[] popups = {popupPrefab1, popupPrefab2, popupPrefab3};
        GameObject popup = null;
        
        for(int i = 0; i < popups.Length + 2; i++)
        {
            int randomIndex = Random.Range(0, popups.Length);
            if (!popups[randomIndex].activeSelf)
            {
                popup = popups[randomIndex];
                break;
            }
        }

        return popup;
    }
}
