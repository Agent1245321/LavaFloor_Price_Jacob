using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipAnim : MonoBehaviour
{
    // Start is called before the first frame update
    public bool flip;
    void Start()
    {
        this.GetComponent<Animator>().SetBool("Flip" , flip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
