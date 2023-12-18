using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : MonoBehaviour, IEnteractable
{
    private Movement movement;
    public KeyHoleObject Hole;

    private Rigidbody obj;
    private GameObject forwardObj;
    public void Start()
    {
        movement = GameObject.FindWithTag("player").GetComponent<Movement>();
        obj = this.transform.parent.GetComponent<Rigidbody>();
        forwardObj = GameObject.FindWithTag("player").transform.parent.Find("ForwardObject").gameObject;
    }

    bool isPickUped = false;

    
    public void Interact()
    {
        if (isPickUped) 
        {
            this.transform.parent.position = movement.transform.position + new Vector3(0, HoverDistance, 0);
            obj.velocity = (forwardObj.transform.forward * 10) + movement.GetComponent<Rigidbody>().velocity;
            obj.useGravity = true;
            this.transform.parent.GetComponent<Collider>().enabled = true;
        }
        else
        {
            this.transform.parent.position = movement.transform.position + new Vector3(0, 2* HoverDistance, 0);
            Unlock();
            Hole.IsKeyInHole = false;
            this.transform.parent.GetComponent<Collider>().enabled = false;
           
            
            
            obj.useGravity = false;
        }
        isPickUped = !isPickUped;

       
    }

   

    public void OnTriggerStay(Collider player)
    {
       // Debug.Log(player.name + "Has Entered Range");
       
        
        if (player.tag == "player")
        {
            //Debug.Log("In Range");
            movement.targetObj = this.gameObject;
            
        }
    }

    public void OnTriggerExit(Collider player)
    {
       // Debug.Log(player.name + "Has Left Range");

        if (player.tag == "player")
        {
            //Debug.Log("Out Of Range");
            movement.targetObj = null;
        }
    }

    public float HoverDistance;
    public void Update()
    {
        this.transform.localPosition = Vector3.zero;
        if(isPickUped)
        {
            this.transform.parent.position = movement.transform.position + new Vector3 (0, HoverDistance, 0);
        }
    }

    public void Lock(GameObject hole)
    {
        Debug.Log("Locking Key");
        obj.useGravity = false;
        obj.constraints = RigidbodyConstraints.FreezeAll;
        this.transform.parent.position = hole.transform.position;
        this.transform.parent.rotation = hole.transform.rotation;


    }

    public void Unlock()
    {
        Debug.Log("Unlocking Key");
        obj.constraints = RigidbodyConstraints.None;
        
    }
}
