using UnityEngine;
using UnityEngine.UI;

public class MarketBubble : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float plug = Mathf.Lerp(0.05f, 0.4f, (GameManager.instance.risk / 100f));
        this.transform.localScale = new Vector3(plug, plug, plug);
        
        //shift color from white to red as risk increases
        Color color = Color.Lerp(Color.white, Color.red, (GameManager.instance.risk / 100f));
        
        //set the color of the bubble
        this.GetComponent<Image>().color = color;
    }
}
