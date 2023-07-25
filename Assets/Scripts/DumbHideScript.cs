using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumbHideScript : MonoBehaviour
{
    public static bool hide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hide == true)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
