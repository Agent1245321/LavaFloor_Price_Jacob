using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoleObject : MonoBehaviour
{

    public GameObject Connection;
    public GameObject Key;
    public KeyObject keyScript;
    public IEvent connected;
    public bool isDoorOpen;
    public bool IsKeyInHole;

    public void Start()
    {
       if( Connection.TryGetComponent(out IEvent target))
        {
            connected = target;
        }
       else
        {
            Debug.LogError("Could Not Get IEvent Script");
        }
        keyScript = Key.GetComponent<KeyObject>();
        keyScript.Hole = this;
}

    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject == Key) 
        {
            Debug.Log("Key Inserted");
            isDoorOpen = true;
            
            connected.Activate();
            keyScript.Lock(this.gameObject);
           
        }

    }

    
    public void Update()
    {
        if(!IsKeyInHole && isDoorOpen) 
        {
            isDoorOpen = false;
            connected.Activate();   
        }
    }




}


