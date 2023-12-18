using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEvent
{
    public bool IsActive { get; set; }
    public void Activate()
    {

    }

    
}
