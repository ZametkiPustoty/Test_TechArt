using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAll : MonoBehaviour
{
    public GameObject Box;

    private void ExecuterTrigger(string trigger)
    {
        if (Box != null)
        {
            var animator = Box.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(trigger);
            }

        }
        
    }
    public void OnOpenButtonClick()
    {
        ExecuterTrigger("TrOpen");
    }
    public void OnCloseButtonClick()
    {
        ExecuterTrigger("TrClose");
    }
}