using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPickUpAble 
{
    void PickUp() { }
    void Throw() { }
    void OnTriggerEnter(Collider player);

}
