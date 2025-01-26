using UnityEngine;

public class InvestorScript : MonoBehaviour
{
    public Investor investor;
    public Topics topic;

    public Investor[] investorList;
    public float timer = 15f;

    void Awake()
    {
        investor = investorList[Random.Range(0, investorList.Length)];
        topic = investor.GetTopic();
    }

    public Sprite GetInvestorImage()
    {
        return investor.image;
    }

    public Topics GetTopic()
    {
        return topic;
    }

    public string GetName()
    {
        return investor.name;
    }

    public float GetRomance(float money)
    {
        return investor.GetRomance(money);
    }

    public float GetFirm(float money)
    {
        return investor.GetFirm(money);
    }

    public float GetLie(float money)
    {
        return investor.GetLie(money);
    }

    public float GetTalkSpeed()
    {
        return investor.talkSpeed;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            //AngryHangUp();
        }
    }


}
