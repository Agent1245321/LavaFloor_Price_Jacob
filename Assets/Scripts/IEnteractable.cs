using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnteractable 
{
   public void Interact() { }
    void OnTriggerStay(Collider player);

    void OnTriggerExit(Collider player);

}
