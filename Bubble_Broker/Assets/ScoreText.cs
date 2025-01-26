using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreText : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Format to dollars
        text.text = "$" + GameManager.instance.money.ToString("N0");
    }

    public void ResetGame()
    {
        GameManager.instance.DestroyThyself();
        SceneManager.LoadScene("Office");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
