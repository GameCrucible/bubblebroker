using UnityEngine;
using UnityEngine.UI;

public class PhoneButton : MonoBehaviour
{
    public Animator animator;
    
    public void TurnOnCanvas()
    {
        animator.SetBool("PhoneIn", true);
    }
}
