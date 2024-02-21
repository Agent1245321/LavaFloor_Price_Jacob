using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public MeshRenderer player;
    public Material deflt;
    public void BuySkin()
    {

    }
    public void SetSkin(ToggleSkin tglScript)
    {
        if(tglScript.tgl.isOn == true)
        {
            player.material = tglScript.matt;
        }
        else 
        { 
            player.material = deflt; 
        }
        
    }
}
