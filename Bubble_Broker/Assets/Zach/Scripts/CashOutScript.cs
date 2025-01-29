using UnityEngine;

public class CashOutScript : MonoBehaviour
{
    public GameObject cashOutButton;
    public GameObject gameOver;
    public GameObject phone;
    public GameObject computer;
    public GameObject mainCanvas;
    public GameObject gameMechanics;
    public GameObject goodEnd;

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
        GameManager.instance.endScreen = true;
        gameOver.SetActive(true);
        cashOutButton.SetActive(false);
        goodEnd.SetActive(true);
        phone.SetActive(false);
        computer.SetActive(false);
        mainCanvas.SetActive(false);
        gameMechanics.SetActive(false);
    }
}
