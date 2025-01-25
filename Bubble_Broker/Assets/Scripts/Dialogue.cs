using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Data;
using UnityEngine.Rendering.Universal;

public class Dialogue : MonoBehaviour
{
    //Investors
    public Investor[] investors;
    public Investor currentInvestor;

    public TMP_Text dialogueText;
    private Topics currentTopic;

    private float money = 0;

    public Image dialogueImage;

    private IEnumerator typingCoroutine;
    private float typewriterPerChar = 0.05f;
    private string typingText;

    public TMP_Text firmText;
    public TMP_Text lieText;
    public TMP_Text romanceText;

    private void Start()
    {
        //PickInvestor();
        typingCoroutine = TypeText();
    }

    public void PickInvestor()
    {
        if (currentInvestor != null)
        {
            Debug.Log("Call is already being taken");
            return;
        }
        if(Random.Range(0, 9) == 0)
        {
            LoadScammer();
        }
        else
        {
            currentInvestor = investors[Random.Range(0,investors.Length)];
            dialogueImage.sprite = currentInvestor.image;
            currentTopic = currentInvestor.GetTopic();
            typingText = currentInvestor.name + " wants to invest in: " + currentTopic.name;
            StartTyping();
        }
    }

    public void StartTyping()
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        ClearPlayerResponses();
        typingCoroutine = TypeText();
        StartCoroutine(typingCoroutine);
    }

    public void LoadScammer()
    {
        Debug.Log("Scammer loaded");
    }

    public void FirmOption()
    {
        if (currentInvestor!=null)
        {
            money += currentInvestor.GetFirm(currentTopic.topicValue);
            Debug.Log(money);
            HangUp();
        }
        
    }

    public void LieOption()
    {
        if (currentInvestor != null)
        {
            money += currentInvestor.GetLie(currentTopic.topicValue);
            Debug.Log(money);
            HangUp();
        }
        
    }

    public void RomanceOption()
    {
        if(currentInvestor != null)
        {
            money += currentInvestor.GetRomance(currentTopic.topicValue);
            Debug.Log(money);
            HangUp();
        }
        
    }

    public void HangUp()
    {
        StopCoroutine(typingCoroutine);
        currentInvestor = null;
        dialogueText.text = "";
        ClearPlayerResponses();
        dialogueImage.sprite = null;
    }

    public float GetTopicPrice(string topicName)
    {
        return currentTopic.topicValue;
    }

    public void SetPlayerResponses()
    {
        firmText.text = currentTopic.firmResponse;
        lieText.text = currentTopic.lieResponse;
        romanceText.text = currentTopic.romanceResponse;
    }

    public void ClearPlayerResponses()
    {
        firmText.text = "...";
        lieText.text = "...";
        romanceText.text = "...";
    }

    IEnumerator TypeText()
    {
        Debug.Log(currentTopic);
        foreach(char letter in typingText)
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typewriterPerChar);
        }
        SetPlayerResponses();
        StopCoroutine(typingCoroutine);
    }
}
