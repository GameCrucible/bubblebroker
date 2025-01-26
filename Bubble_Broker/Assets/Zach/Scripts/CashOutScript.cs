using UnityEngine;

public class CashOutScript : MonoBehaviour
{
    public GameObject cashOutButton;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cashOutButton.SetActive(GameManager.instance.currentQuarter == 4);
    }

    public void CashOut()
    {
        GameManager.instance.GameOver();
    }
}
