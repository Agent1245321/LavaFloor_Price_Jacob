using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : Grabable
{
   
    public KeyHoleObject Hole;

    
    public  override void Interact()
    {
        Unlock();
        Grab();
            
            if(Hole != null)
            {
                Hole.IsKeyInHole = false;
            }

        
    }

   

   

   
    public override void Update()
    {
        Move();
        if(Hole != null && Hole.IsKeyInHole)
        {
                this.transform.rotation = Hole.transform.rotation;  
        }
    }

    public void Lock(GameObject hole)
    {
        Debug.Log("Locking Key");
        obj.useGravity = false;
        obj.constraints = RigidbodyConstraints.FreezeAll;
        this.transform.position = hole.transform.position;
        this.transform.rotation = hole.transform.rotation;


    }

    public void Unlock()
    {
        Debug.Log("Unlocking Key");
        obj.constraints = RigidbodyConstraints.None;
        
    }

}
