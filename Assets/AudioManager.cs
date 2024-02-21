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

    public AudioClip[] actionSoundsA;
    public string[] actionSoundTags;
    public Dictionary<string, AudioClip> actionSounds = new Dictionary<string, AudioClip>();

    public void Awake()
    {
        for(int i = 0; i < actionSoundsA.Length; i++)
        {
            actionSounds.Add(actionSoundTags[i], actionSoundsA[i]);
        }
    }
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

    public void PlayActionSound(string a)
    {
        Debug.Log("Playing Action Sound");
        Debug.Log(sound);
        Debug.Log(actionSounds);
        Debug.Log(actionSounds[a]);
       // Debug.Log("Playing action sound" + a + " " + actionSounds[a]);
        sound.clip = actionSounds[a];
        sound.Play();
    }
}
