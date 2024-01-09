using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonEvent : MonoBehaviour, IEvent
{
    public bool IsActive { get; set; } = false;
    public Animator animator;
      public void Activate()
    {
       
        if ( IsActive )
        {
            TurnOn();
        }
        else
        {
            TurnOff();
        }
    }

    public void TurnOn()
    {
        Debug.Log("TurningOnPiston");
        //IsActive = true;
        animator.SetBool("IsOn", true);

    }

    public void TurnOff()
    {
        Debug.Log("TurningOffPiston");
        //IsActive = false;
        animator.SetBool("IsOn", false);
    }

}
