using UnityEngine;

[CreateAssetMenu(fileName = "Investor", menuName = "Scriptable Objects/Investor")]
public class Investor : ScriptableObject
{
    public Topics[] topic;

    public Sprite image;

    public Sprite computerImage;

    public float firmMultiplier;
    public float lieMultiplier;
    public float romanceMultiplier;

    public int romance;
    public float talkSpeed;

    public float GetFirm(float normalPrice){  return normalPrice * firmMultiplier;}

    public float GetLie(float normalPrice) {  return normalPrice * lieMultiplier;}

    public float GetRomance(float normalPrice) {
        float returnRomanceValue = normalPrice * (romanceMultiplier * romance);
        return returnRomanceValue;
     }

    public Topics GetTopic()
    {
        return topic[Random.Range(0, topic.Length)];
    }
    
}
