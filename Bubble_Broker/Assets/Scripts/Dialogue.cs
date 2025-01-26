using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Data;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Dialogue : MonoBehaviour
{
    //Investors
    [NonSerialized]
    public Investor[] investors; //Get from GameManager
    public InvestorScript currentInvestor;

    public GameObject[] investorsOnLine;
    public GameObject investorPrefab;

    public TMP_Text dialogueText;
    private Topics currentTopic;

    public Animator animator;

    //private float money = 0; //Get from GameManager

    public Image dialogueImage;

    private IEnumerator typingCoroutine;
    //private float typewriterPerChar = 0.05f;
    private string typingText;

    public TMP_Text firmText;
    public TMP_Text lieText;
    public TMP_Text romanceText;
    public TMP_Text moneyText;

    public AudioSource audio;
    public AudioSource audioClose;
    public AudioSource loseMoney;
    public AudioSource earnMoney;

    public Image patienceProgress;

    private float timer;
    private bool doneTalking;

    private void Start()
    {
        //Get investors from GameManager
        investors = GameManager.instance.investors.ToArray();

        timer = 5f;
        doneTalking = false;

        //PickInvestor();
        typingCoroutine = TypeText();
    }

    private void Update()
    {
        if (doneTalking)
        {
            timer -= Time.deltaTime;
            patienceProgress.fillAmount = (timer / 10f);
            if (timer < 0)
            {
                doneTalking = false;
                GameManager.instance.risk += GameManager.instance.currentQuarter; //Increase risk if call is not picked up
                HangUp();
            }
        }
    }

    public void PickInvestor()
    {
        if (currentInvestor != null)
        {
            Debug.Log("Call is already being taken");
            return;
        }
        else
        {
            currentInvestor = Instantiate(investorPrefab).GetComponent<InvestorScript>();
            dialogueImage.sprite = currentInvestor.GetInvestorImage();
            currentTopic = currentInvestor.GetTopic();
            typingText = "<b>" + currentInvestor.GetName() + "</b>\n" + currentTopic.investorLine;
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
            if (currentInvestor.investor.dislike == Investor.Choices.Firm)
            {
                GameManager.instance.risk += (int)(Random.Range(5, 10));
                //*GameManager.instance.currentQuarter * .6f
                HangUp();
                loseMoney.Play();
                return;
            }
            
            GameManager.instance.money += (int)currentInvestor.GetFirm(currentTopic.topicValue);
            moneyText.text = "" + GameManager.instance.money;
            HangUp();
            earnMoney.Play();
        }
        
    }

    public void LieOption()
    {
        if (currentInvestor.investor.dislike == Investor.Choices.Lie)
        {
            GameManager.instance.risk += (int)(Random.Range(5, 10));
            //*GameManager.instance.currentQuarter * .6f
            HangUp();
            loseMoney.Play();
            return;
        }
        
        if (currentInvestor != null)
        {
            GameManager.instance.money += (int)currentInvestor.GetLie(currentTopic.topicValue);
            moneyText.text = "" + GameManager.instance.money;
            HangUp();
            earnMoney.Play();
        }
        
    }

    public void RomanceOption()
    {
        if (currentInvestor.investor.dislike == Investor.Choices.Romance)
        {
            GameManager.instance.risk += (int)(Random.Range(5, 10));
            //*GameManager.instance.currentQuarter * .6f
            HangUp();
            loseMoney.Play();
            return;
        }
        
        if(currentInvestor != null)
        {
            GameManager.instance.money += (int)currentInvestor.GetRomance(currentTopic.topicValue);
            moneyText.text = "" + GameManager.instance.money;
            currentInvestor.investor.romance += Random.Range(5, 10); //Increase investor romance
            HangUp();
            earnMoney.Play();
        }
        
    }

    public void HangUp()
    {
        StopCoroutine(typingCoroutine);
        audio.Stop();
        currentInvestor = null;
        dialogueText.text = "";
        ClearPlayerResponses();
        dialogueImage.sprite = null;
        doneTalking = false;
        patienceProgress.fillAmount = 1f;
        timer = 10f;
        animator.SetBool("PhoneIn", false);
        audioClose.Play();
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
        doneTalking = true;
        timer = 10f;
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
            if (!(audio.isPlaying))
            {
                audio.Play();
            }
            dialogueText.text += letter;
            yield return new WaitForSeconds(currentInvestor.GetTalkSpeed());
        }
        SetPlayerResponses();
        audio.Stop();
        StopCoroutine(typingCoroutine);
    }
}
