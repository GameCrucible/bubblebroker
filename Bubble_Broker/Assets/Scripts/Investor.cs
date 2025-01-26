using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

    public Choices dislike;

    public float GetFirm(float normalPrice){  return normalPrice * firmMultiplier;}

    public float GetLie(float normalPrice) {  return normalPrice * lieMultiplier;}

    public float GetRomance(float normalPrice) 
    {
        float clampedRomance = Mathf.Clamp(romance, 0, 100);
        romanceMultiplier = Mathf.Lerp(0.5f, 2.0f, clampedRomance / 100f);
        
        float returnRomanceValue = normalPrice * romanceMultiplier;
        return returnRomanceValue;
     }

    public Topics GetTopic()
    {
        return topic[Random.Range(0, topic.Length)];
    }
    
    public enum Choices
    {
        Firm,
        Lie,
        Romance,
        None
    }
    
}
