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

    public AudioClip[] loops;

    public string[] actionSoundTags;


    public Dictionary<string, AudioClip> actionSounds = new Dictionary<string, AudioClip>();

    public void Awake()
    {
        for(int i = 0; i < actionSoundsA.Length; i++)
        {
            actionSounds.Add(actionSoundTags[i], actionSoundsA[i]);
            movesound.Play();
            movesound.loop = true;
        }
    }

    public void Update()
    {
        UpdateMoveSound();
    }
    public AudioSource sound;
    public AudioSource aSound;
    public AudioSource movesound;
    public AudioSource music;

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
                sound.clip = thump[0];
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
        aSound.clip = actionSounds[a];
        aSound.Play();
    }

    float speed;
    public Rigidbody rb;
    public void UpdateMoveSound()
    {
        speed = rb.velocity.magnitude;
        if (speed > 1)
        {
            movesound.volume = (speed + 50) / 500;
            movesound.pitch = speed / 500;
        }
        else { movesound.volume = 0;  movesound.pitch = 0; }
    }
}
