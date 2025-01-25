using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class InvestorManager : MonoBehaviour
{
    [Header("UI References")]
    public Image investorImage;
    public TextMeshProUGUI investorName;
    public TextMeshProUGUI investorLikes;
    public TextMeshProUGUI investorDislikes;
    public TextMeshProUGUI investorRomance;
    public TextMeshProUGUI pageText;
    
    public Button nextButton;
    public Button previousButton;

    private int currentIndex = 0;

    private void Start()
    {
        // Initial check for investors
        if (GameManager.instance.investors.Count > 0)
        {
            LoadInvestor(GameManager.instance.investors[currentIndex]);
            UpdateNavigationButtons();
            UpdatePageText();
        }
        else
        {
            Debug.LogWarning("No investors found in GameManager");
        }

        // Add button listeners
        nextButton.onClick.AddListener(NextInvestor);
        previousButton.onClick.AddListener(PreviousInvestor);
    }

    public void LoadInvestor(Investor investor)
    {
        investorImage.sprite = investor.image;
        investorName.text = investor.name;

        // Build likes string
        string topics = "I like to invest in ";
        foreach (var topic in investor.topic)
        {
            topics += topic.name + ", ";
        }
        if (investor.topic.Length > 0)
        {
            topics = topics.TrimEnd(',', ' ') + " companies";
        }
        investorLikes.text = topics;

        // Handle dislikes
        if (investor.firmMultiplier <= 0)
        {
            investorDislikes.text = "I don't like it when companies are too firm";
        }
        else if (investor.romanceMultiplier <= 0)
        {
            investorDislikes.text = "I don't like it when people are flirty";
        }
        else
        {
            investorDislikes.text = "I don't like it when companies are dishonest";
        }
        
        investorRomance.text = $"Romance: {investor.romance * 100}%";
    }

    private void NextInvestor()
    {
        if (currentIndex < GameManager.instance.investors.Count - 1)
        {
            currentIndex++;
            LoadInvestor(GameManager.instance.investors[currentIndex]);
            UpdateNavigationButtons();
            UpdatePageText();
        }
    }

    private void PreviousInvestor()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            LoadInvestor(GameManager.instance.investors[currentIndex]);
            UpdateNavigationButtons();
            UpdatePageText();
        }
    }

    private void UpdateNavigationButtons()
    {
        previousButton.interactable = currentIndex > 0;
        nextButton.interactable = currentIndex < GameManager.instance.investors.Count - 1;
    }

    private void UpdatePageText()
    {
        pageText.text = $"{currentIndex + 1}/{GameManager.instance.investors.Count}";
    }

    private void OnDestroy()
    {
        // Clean up button listeners
        nextButton.onClick.RemoveListener(NextInvestor);
        previousButton.onClick.RemoveListener(PreviousInvestor);
    }
}
