using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
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
    public GameObject goodEnd;
    public GameObject badEnd;
    public GameObject Day1;
    public GameObject q1;
    public GameObject q2;
    public GameObject q3;
    public GameObject q4;

    [NonSerialized] public int risk = 0;
    [NonSerialized] public float dayTick = 2;
    
    [Tooltip("In game days for a quarter to pass.")]
    [Range(60, 90)]
    public int fiscalQuarterLength = 60;
    
    [NonSerialized] public int currentQuarter = 1;
    [NonSerialized] public int currentDay = 1;
    [NonSerialized] public int money = 0;
    
    [Tooltip("Starting money for the player.")]
    public int initialMoney = 10000;
    
    public List<Investor> investors = new List<Investor>();

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
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    private IEnumerator UpdateDay()
    {
        while (risk < 30)
        {
            yield return new WaitForSeconds(dayTick);  //Time range for days
            currentDay++;
        
            if (currentDay % fiscalQuarterLength == 0 && currentDay != 0)
            {
                currentQuarter++;
                currentDay = 1;
                risk += Random.Range(1, 5);
            }
            
            risk = Mathf.Clamp(risk, 0, 100);
            
            Debug.Log("Risk: " + risk);
        }


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
        gameOver.SetActive(true);
        badEnd.SetActive(true);
        phone.SetActive(false);
        computer.SetActive(false);
        mainCanvas.SetActive(false);
        gameMechanics.SetActive(false);
    }
}
