using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEvent : MonoBehaviour, IEvent
{
    public bool isOpen = false;
      public void Activate()
    {
        if ( isOpen )
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
        isOpen = true;
    }

    public void CloseDoor()
    {
        Debug.Log("Closing Door");
        isOpen = false;
    }

}
