using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Vars
    public static GameManager instance;
    public GameObject gameOver;
    public GameObject phone;
    public GameObject computer;
    public GameObject mainCanvas;
    public GameObject gameMechanics;
    public GameObject badEnd;
    // public GameObject Day1;
    // public GameObject q1;
    // public GameObject q2;
    // public GameObject q3;
    // public GameObject q4;

    public AudioSource upRisk;

    [NonSerialized] public int risk = 0;
    [NonSerialized] public float dayTick = 2;
    public int currentRisk = 0;
    
    [Tooltip("In game days for a quarter to pass.")]
    [Range(30, 90)]
    public int fiscalQuarterLength = 45;
    
    [NonSerialized] public int currentQuarter = 1;
    [NonSerialized] public int currentDay = 1;
    public int money = 0;
    
    [Tooltip("Starting money for the player.")]
    public int initialMoney = 100;
    
    public List<Investor> investors = new List<Investor>();

    [Header("Quarter Sticky")] 
    public Image sticky;
    public TextMeshProUGUI stickyText;
    public Sprite q1Texture;
    public Sprite q2Texture;
    public Sprite q3Texture;
    public Sprite q4Texture;

    [Header("Boss Notes")] 
    public GameObject d1Report;
    public GameObject q1Report;
    public GameObject q2Report;
    public GameObject q3Report;
    public GameObject q4Report;
    
    [NonSerialized] public bool endScreen = false;
    

    //Ensure only one instance of the GameManager exists
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }
    
    public void DestroyThyself()
    {
        Destroy(gameObject);
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        money = initialMoney;
        
        StartCoroutine(UpdateDay());
        
        ResetInvestors();
        
        RandomizeInvestorStats();
        
        StartCoroutine(displayNotes());
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private IEnumerator UpdateDay()
    {
        while (risk < 100 && !endScreen)
        {
            yield return new WaitForSeconds(dayTick);  //Time range for days
            currentDay++;

            if (currentRisk < risk)
            {
                currentRisk = risk;
                upRisk.Play();
            }
        
            if (currentDay % fiscalQuarterLength == 0 && currentDay != 0)
            {
                currentQuarter++;
                currentDay = 1;
                risk -= Random.Range(1, 3);
                
                //Reset Q5 to Q1
                if (currentQuarter == 5)
                {
                    currentQuarter = 1;
                    stickyText.text = "Q1";
                    sticky.sprite = q1Texture;
                }
                else if (currentQuarter == 2)
                {
                    stickyText.text = "Q2";
                    sticky.sprite = q2Texture;
                }
                else if (currentQuarter == 3)
                {
                    stickyText.text = "Q3";
                    sticky.sprite = q3Texture;
                }
                else if (currentQuarter == 4)
                {
                    stickyText.text = "Q4";
                    sticky.sprite = q4Texture;
                }
            }
            
            risk = Mathf.Clamp(risk, 0, 100);
            
            Debug.Log("Risk: " + risk);
        }

        //money = (int)(money * Random.Range(0.25f, 0.5f)); //Lose money on pop

        GameOver();
    }

    private void ResetInvestors()
    {
        foreach (var investor in investors)
        {
            investor.romanceMultiplier = 0.5f;
            investor.romance = 0;
        }
    }
    
    public void GameOver()
    {
        endScreen = true;
        money = 0;
        gameOver.SetActive(true);
        badEnd.SetActive(true);
        phone.SetActive(false);
        computer.SetActive(false);
        mainCanvas.SetActive(false);
        gameMechanics.SetActive(false);
    }

    private void RandomizeInvestorStats()
    {
        foreach (var investor in investors)
        {
            if (investor.name == "Scammer" || investor.name == "Milton Stonefella")
                return;
            
            investor.dislike = (Investor.Choices) Random.Range(0, 3);
        }
    }

    private IEnumerator displayNotes()
    {
        yield return new WaitForSeconds(3f);
        d1Report.SetActive(true);
        
        //Wait until d1 report is closed
        yield return new WaitUntil(() => !d1Report.activeSelf);

        yield return new WaitForSeconds(5f);
        
        q1Report.SetActive(true);
        
        //Wait until q1 report is closed
        yield return new WaitUntil(() => !q1Report.activeSelf);
        
        //Wait until quarter 2
        yield return new WaitUntil(() => currentQuarter >= 2);
        
        q2Report.SetActive(true);
        
        //Wait until q2 report is closed
        yield return new WaitUntil(() => !q2Report.activeSelf);
        
        //Wait until quarter 3
        yield return new WaitUntil(() => currentQuarter >= 3);
        
        q3Report.SetActive(true);
        
        //Wait until q3 report is closed
        yield return new WaitUntil(() => !q3Report.activeSelf);
        
        //Wait until quarter 4
        yield return new WaitUntil(() => currentQuarter >= 4);
        
        q4Report.SetActive(true);
    }
}
