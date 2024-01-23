using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour, IEvent
{
    public bool IsActive { get; set; } = false;
    public Animator animator;
      public void Activate()
    {
       
        if ( IsActive )
        {
            OpenDoor();
        }
        else
        {
            CloseDoor();
        }
    }

    public void OpenDoor()
    {
        Debug.Log("Opening Door");
        //IsActive = true;
        animator.SetBool("IsOpen", true);

    }

    public void CloseDoor()
    {
        Debug.Log("Closing Door");
        //IsActive = false;
        animator.SetBool("IsOpen", false);
    }

}
