using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSkin : MonoBehaviour
{
    public Toggle tgl;
    public Material matt;
    public int cost;
    public Button buyBtn;
    public TextMeshProUGUI costT;
    public Image displlay;
    public Sprite img;
    public int skIndex;
    

    private void Awake()
    {
         tgl = this.GetComponent<Toggle>();
        costT.text = cost.ToString();
        displlay.sprite = img;
    }
}
