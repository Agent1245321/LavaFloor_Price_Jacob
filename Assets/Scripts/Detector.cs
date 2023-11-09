using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Detector : MonoBehaviour
{
    public static bool triggerOnWall;
    private Movement move;
    // Start is called before the first frame update
    void Start()
    {
        move = this.transform.root.GetComponentInChildren<Movement>();
    }


   public void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Wall")
        {
            
            triggerOnWall = false;
            move.isOnWall = false;
            Debug.Log($"Trigger Left Wall");
        }
        
    }

    public void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Wall")
        {
            triggerOnWall = true;
            Debug.Log($"Trigger Entered Wall");
        }

    }

}
