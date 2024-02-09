using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlPanelSequence : MonoBehaviour
{


    public TextMesh tmp1;
    public TextMesh tmp2;

    public KeyHoleObject kH;
    public BigRedButton bRB; 





    public void UpdateScreens(int i)
    {
        switch (i)
        
        { 
        
            case 0:
                tmp1.text = ("Insert Key...");
                tmp2.text = (tmp1.text);
                break;

                case 1:
                tmp1.text = ("Awaiting Input...");
                tmp2.text = (tmp1.text);
                break;

                case 2:
                tmp1.text = ("Activated");
                tmp2.text = (tmp1.text);
                break;

                default:
                tmp1.text = ("ERROR");
                tmp2.text = (tmp1.text);
                break;
        }
    }


    public void Update()
    {
        if(bRB.connected.IsActive) 
        { UpdateScreens(2); }
        else if(kH.IsKeyInHole)
        {
            
            UpdateScreens(1);
        }
    }




}

