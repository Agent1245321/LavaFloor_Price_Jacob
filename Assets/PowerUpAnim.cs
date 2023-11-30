using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAnim : MonoBehaviour
{
    Animator anim;

    void Start()
    { 
        anim = this.transform.root.GetComponentInChildren<Animator>();       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
          
            Debug.Log("Collected!");
            anim.SetBool("IsCollected", true);
            this.transform.root.parent = other.transform.root.Find("Postion Follower");
            this.transform.parent.position = other.transform.root.Find("Postion Follower").position + new Vector3(0, .5f, 0);
            StartCoroutine(DestroySelf());
            
        }
    }

    public IEnumerator DestroySelf()
    { 
        yield return new WaitForSeconds(3);

        Destroy(this.transform.parent.gameObject);

    }

    
}
