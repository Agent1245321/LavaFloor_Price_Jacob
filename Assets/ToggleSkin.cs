using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSkin : MonoBehaviour
{
    public Toggle tgl;
    public Material matt;
    public int cost;
    public Button buyBtn;

    private void Awake()
    {
         tgl = this.GetComponent<Toggle>();
    }
}
