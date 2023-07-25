using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    private Movement move;
    // Start is called before the first frame update
    void Start()
    {
        move = this.transform.root.GetComponentInChildren<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void OnTriggerExit(Collider other)
    {
        Debug.Log("Triggered");
        Debug.Log(other.gameObject.tag);
        Debug.Log(other.name);
        if (other.gameObject.tag == "Wall") move.isOnWall = false;
        
    }
}
