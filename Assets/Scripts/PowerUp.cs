using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;
using UnityEngine;



public class PowerUp : MonoBehaviour
{
    public int powerUp;

    private int powerUpSetter;
    public Sprite[] icons;
    public Material[] balls;
    private PowerUpManager Manager;


    private SpriteRenderer icon;
    private MeshRenderer ball;
    public void UpdateButtons()
    {
        /* Debug.Log($"Power up {transform.root.name} has powerupSetter of {powerUpSetter}");
         Debug.Log($"Power up {transform.root.name} has powerUP of {powerUp}");
         powerUpSetter = powerUp;
         Debug.Log($"Power up {transform.root.name} has powerupSetter of {powerUpSetter}");
         Debug.Log($"Power up {transform.root.name} has powerUP of {powerUp}");
        */

        icon = transform.GetComponentInChildren<SpriteRenderer>();
        ball= transform.GetComponentInChildren<MeshRenderer>();
        if ( powerUp <= icons.Length || powerUp <= balls.Length)
        {
            Debug.Log("Matterials Setting");
            icon.sprite = icons[powerUp];
            ball.material = balls[powerUp];
        }
        else
        { 
            Debug.LogError("Lacking Matts/Icons to set powerup");
            Debug.LogError($"Icons: {icons.Length}");
            Debug.LogError($"Balls: {balls.Length}");
        }
    }

    public void Start()
    {
      /*  Debug.Log($"Power up {transform.root.name} has powerupSetter of {powerUpSetter}");
        Debug.Log($"Power up {transform.root.name} has powerUP of {powerUp}");
      */
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
           Manager = other.GetComponent<PowerUpManager>();
            Debug.Log($"Preparing To Set Status To {powerUp}");
            Manager.SetStatus(powerUp);


        }
    }

    
}
