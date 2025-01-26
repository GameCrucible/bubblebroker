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
    
    public Image investorImage2;
    public TextMeshProUGUI investorName2;
    public TextMeshProUGUI investorLikes2;
    public TextMeshProUGUI investorDislikes2;
    public TextMeshProUGUI investorRomance2;
    
    public Image investorImage3;
    public TextMeshProUGUI investorName3;
    public TextMeshProUGUI investorLikes3;
    public TextMeshProUGUI investorDislikes3;
    public TextMeshProUGUI investorRomance3;
    
    public TextMeshProUGUI pageText;
    public Button nextButton;
    public Button previousButton;

    private int currentPageIndex = 0;
    private int investorsPerPage = 3;

    private void Start()
    {
        InitializeUI();
        nextButton.onClick.AddListener(NextPage);
        previousButton.onClick.AddListener(PreviousPage);
    }

    private void InitializeUI()
    {
        if (GameManager.instance.investors.Count == 0)
        {
            Debug.LogWarning("No investors found in GameManager");
            DisableAllUIElements();
            return;
        }

        LoadPage(currentPageIndex);
        UpdateNavigation();
        UpdatePageCounter();
    }

    private void LoadPage(int pageIndex)
    {
        int startIndex = pageIndex * investorsPerPage;
        
        // Load first investor
        SetInvestorUI(startIndex, investorImage, investorName, investorLikes, investorDislikes, investorRomance);
        
        // Load second investor
        SetInvestorUI(startIndex + 1, investorImage2, investorName2, investorLikes2, investorDislikes2, investorRomance2);
        
        // Load third investor
        SetInvestorUI(startIndex + 2, investorImage3, investorName3, investorLikes3, investorDislikes3, investorRomance3);
    }

    private void SetInvestorUI(int index, Image image, TextMeshProUGUI nameText, 
                             TextMeshProUGUI likes, TextMeshProUGUI dislikes, TextMeshProUGUI romance)
    {
        bool isValidIndex = index < GameManager.instance.investors.Count;
        
        image.gameObject.SetActive(isValidIndex);
        nameText.gameObject.SetActive(isValidIndex);
        likes.gameObject.SetActive(isValidIndex);
        dislikes.gameObject.SetActive(isValidIndex);
        romance.gameObject.SetActive(isValidIndex);

        if (!isValidIndex) return;

        Investor investor = GameManager.instance.investors[index];
        image.sprite = investor.computerImage;
        nameText.text = investor.name;

        // Build likes string
        string topics = "I like to invest in ";
        if (investor.topic.Length > 0)
        {
            foreach (var topic in investor.topic)
            {
                topics += topic.name + ", ";
            }
            topics = topics.TrimEnd(',', ' ') + " companies";
        }
        likes.text = topics;

        // Handle dislikes
        dislikes.text = GetDislikeText(investor);
        romance.text = $"Romance: {investor.romance}%";
    }

    private string GetDislikeText(Investor investor)
    {
        switch (investor.dislike)
        {
            case Investor.Choices.Firm:
                return "I don't like it when companies are too firm";
            case Investor.Choices.Lie:
                return "I don't like it when companies are dishonest";
            case Investor.Choices.Romance:
                return "I don't like it when people are flirty";
        }
        
        return "I don't have any dislikes";
    }

    private void NextPage()
    {
        currentPageIndex++;
        LoadPage(currentPageIndex);
        UpdateNavigation();
        UpdatePageCounter();
    }

    private void PreviousPage()
    {
        currentPageIndex--;
        LoadPage(currentPageIndex);
        UpdateNavigation();
        UpdatePageCounter();
    }

    private void UpdateNavigation()
    {
        previousButton.interactable = currentPageIndex > 0;
        nextButton.interactable = (currentPageIndex + 1) * investorsPerPage < GameManager.instance.investors.Count;
    }

    private void UpdatePageCounter()
    {
        int totalPages = Mathf.CeilToInt((float)GameManager.instance.investors.Count / investorsPerPage);
        pageText.text = $"{currentPageIndex + 1}/{totalPages}";
    }

    private void DisableAllUIElements()
    {
        investorImage.gameObject.SetActive(false);
        investorImage2.gameObject.SetActive(false);
        investorImage3.gameObject.SetActive(false);
        
        // Disable all text elements
        investorName.gameObject.SetActive(false);
        investorLikes.gameObject.SetActive(false);
        investorDislikes.gameObject.SetActive(false);
        investorRomance.gameObject.SetActive(false);
        
        investorName2.gameObject.SetActive(false);
        investorLikes2.gameObject.SetActive(false);
        investorDislikes2.gameObject.SetActive(false);
        investorRomance2.gameObject.SetActive(false);
        
        investorName3.gameObject.SetActive(false);
        investorLikes3.gameObject.SetActive(false);
        investorDislikes3.gameObject.SetActive(false);
        investorRomance3.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        nextButton.onClick.RemoveAllListeners();
        previousButton.onClick.RemoveAllListeners();
    }
}
