using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameObject spawn;
    private Animator anim;
    [SerializeField]
    public static bool clear;
    private bool thisIsTrue;
    public static bool isDead;
    
    // Start is called before the first frame update
    void Start()
    {

        spawn = GameObject.FindWithTag("spawn");
        anim = this.transform.root.GetComponentInChildren<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(isDead)
        {
            anim.SetBool("Did Die", true);
            StartCoroutine(Died());

        }

        if (clear == true) 
        {
           
            if (!thisIsTrue)
            {
                anim.SetBool("Is Collected", false); 
            }
            else
            {
                StartCoroutine(IDFK());
            }
            
            
            

        }
    }

    private void OnTriggerEnter(Collider other)
    {
            if(other.tag == "player")
        {
            thisIsTrue= true;
            clear = true;
            
            anim.SetBool("Is Collected", true);
            
            spawn.transform.position = this.transform.position + new Vector3(0, .5f, 0);

            
        }
    }
    public IEnumerator IDFK()
    {
        yield return new WaitForSeconds(1);
        clear = false;
        thisIsTrue = false;
       
    }

    public IEnumerator Died()
    {
        yield return new WaitForSeconds(1);
        isDead = false;
        anim.SetBool("Did Die", false);


    }
}
