using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public MeshRenderer player;
    public Material deflt;
    private GameObject[] toggles;

    public  int crystalCount;
    public TextMeshProUGUI crystalCountT;




    public void BuySkin(ToggleSkin tglScript)
    {
        if(crystalCount >= tglScript.cost)
        {
            tglScript.costT.text = "PURCHASED";
            crystalCount -= tglScript.cost;
            tglScript.buyBtn.interactable = false;
            tglScript.tgl.interactable = true;
            crystalCountT.text = $"Crystals: {crystalCount}";
        }
    }

   

    public void SetSkin(ToggleSkin tglScript)
    {
        toggles = null;
        toggles = GameObject.FindGameObjectsWithTag("Toggle");


        //Checks if the toggle was turned on
        if(tglScript.tgl.isOn == true)
        {
            //Sets all the toggles to off, loop only calls once because it is in the above if statement
            foreach (GameObject t in toggles)
            { 
                //Checks to see if the instance is the same as the selected toggle
                if (t != tglScript.gameObject)
                {
                    //sets the toggle to off/ calling this method again without calling this loop
                    t.GetComponent<Toggle>().isOn = false;
                }
                
            }

            player.material = tglScript.matt;
        }

        //calls when the selected toggle is turned off
        else 
        { 
            player.material = deflt; 
        }
        
    }
}
