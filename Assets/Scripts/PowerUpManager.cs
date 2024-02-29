using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;


[ExecuteInEditMode]
public class PowerUpManager : MonoBehaviour
{

    public Material[] matts;
    private Movement ball;
    public GameObject indicator;

    [SerializeField]
    public static int PowerUpType;
    // Start is called before the first frame update

    private void Start()
    {
        ball= GetComponent<Movement>();
        SetStatus(0);
        
    }
    public void SetStatus(int indx)
    {
        ball = GetComponent<Movement>();
        ball.TryInteract();
       // Debug.Log($"Setting Status to {indx}");
        indicator.SetActive( true );
        indicator.GetComponent<Renderer>().material = matts[indx];
        ball.status = indx;

        if (indx == 0)
        {
          //  Debug.Log("Disabling PowerUp");
           indicator.SetActive(false);
        }

    }


  
}
