using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextScript : MonoBehaviour
{
    GameObject playa;
    float dFP; //distance from playa
    float opacity;
    // Start is called before the first frame update
    void Start()
    {
        playa = GameObject.FindWithTag("MainCamera");
        Debug.Log(playa.name);
    }

    // Update is called once per frame
    void Update()
    {
        dFP = Mathf.Sqrt(Mathf.Pow(playa.transform.position.x - transform.position.x, 2) + Mathf.Pow(playa.transform.position.y - transform.position.y, 2) + Mathf.Pow(playa.transform.position.z - transform.position.z, 2));
        if (dFP >= 11) opacity = 1 / (dFP - 10);
        else if (dFP < 11) { opacity = 1; }

        if (opacity == 1 && this.gameObject.GetComponent<TextMesh>().color.a != 1)
        {
            this.gameObject.GetComponent<TextMesh>().color = new Color(0, 0, 0, 1);
            Debug.Log("Setting Alpha to 1");
        }
        else if (opacity > .08 && opacity < 1 )
        {
            this.gameObject.GetComponent<TextMesh>().color = new Color(0, 0, 0, opacity);
            Debug.Log("Updating Alpha");
        }
        else if(this.gameObject.GetComponent<TextMesh>().color.a != 0 && opacity <= .08)
        {
            this.gameObject.GetComponent<TextMesh>().color = new Color(0, 0, 0, 0);
            Debug.Log("Setting Alpha to 0");
        }
    }

    public void LateUpdate()
    {
        
    }
}
