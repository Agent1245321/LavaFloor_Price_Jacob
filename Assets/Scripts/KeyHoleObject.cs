using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoleObject : MonoBehaviour
{

    public GameObject Connection;
    public GameObject Key;
    private KeyObject keyScript;
    public IEvent connected;
    public Animator Anim;
   
    public bool IsKeyInHole;

    public void Start()
    {
        keyScript = Key.transform.GetComponent<KeyObject>();
        
        if ( Connection.TryGetComponent(out IEvent target))
        {
            connected = target;
        }
       else
        {
            Debug.LogError("Could Not Get IEvent Script");
        }
        
}

    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject == Key) 
        {
            keyScript.Hole = this;
            Debug.Log("Key Inserted");
            if(Anim != null) 
            {
                Anim.SetBool("IsOn", true); 
            }
            
            connected.IsActive = true;
            
            IsKeyInHole = true;
            
            
            
            connected.Activate();
            keyScript.Lock(this.gameObject);
           
        }

    }

    
    public void Update()
    {
        if(!IsKeyInHole && connected.IsActive) 
        {
            connected.IsActive = false;
            connected.Activate();
            if (Anim != null)
            {
                Anim.SetBool("IsOn", false);
            }
        }
    }




}


