using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoleObject : MonoBehaviour
{

    public GameObject Connection;
    public GameObject Key;
    public KeyObject keyScript;
    public IEvent connected;
    public Animator Gen;
   
    public bool IsKeyInHole;

    public void Start()
    {
        keyScript = Key.transform.GetChild(0).GetComponent<KeyObject>();
        keyScript.Hole = this;
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
            Debug.Log("Key Inserted");
            Gen.SetBool("IsOn", true);
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
            Gen.SetBool("IsOn", false);
        }
    }




}


