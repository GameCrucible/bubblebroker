using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Vars
    public static GameManager instance;
    
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private IEnumerator UpdateDay()
    {
        while (risk < 100)
        {
            yield return new WaitForSeconds(dayTick);  //Time range for days
            currentDay++;
            risk += Random.Range(1, 3);
        
            if (currentDay % fiscalQuarterLength == 0 && currentDay != 0)
            {
                currentQuarter++;
                risk -= Random.Range(10, 15);
            }
        }
    }
}
