using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] thump;
    public AudioClip[] tick;
    public AudioClip[] whistle;
    public AudioClip[] click;
    public AudioClip[] clap;
    public AudioClip[] fwoosh;
    public AudioClip[] pop;
    public AudioClip[] snap;
    public AudioClip[] hmm;


    public AudioSource sound;

    private void OnCollisionEnter(Collision other)
    {
        string itm = other.gameObject.tag;
        Debug.Log(itm);
        switch (itm)
        {
            case "Floor":
                sound.clip = thump[0];
                break;

            case "Wall":
                sound.clip = thump[1];
                break;

            case "lava":
                sound.clip= fwoosh[1];
                break;

            case "glass":
                sound.clip = click[0];
                break;

            default:
                sound.clip = hmm[0];
                break;
        }

        sound.Play();
    }
}
