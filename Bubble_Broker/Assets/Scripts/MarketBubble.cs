using UnityEngine;

public class MarketBubble : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float plug = (((float)GameManager.instance.risk) / 100 )+ 0.05f;
        this.transform.localScale = new Vector3(plug, plug, plug);
    }
}
