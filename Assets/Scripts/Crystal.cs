using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    public ParticleSystem particles;
    public GameObject crystal;

    public void Start()
    {
        
        
        crystal = this.transform.Find("prism").gameObject;
        particles = this.transform.GetComponentInChildren<ParticleSystem>();
        particles.Pause();
        Debug.Log("Paused");
        
    }

    public IEnumerator DestroySelf()
    {
        this.gameObject.GetComponent<CapsuleCollider>().enabled = false;
        crystal.SetActive(false);
        particles.Play();
        yield return new WaitForSeconds(3);
        this.gameObject.SetActive(false);
        yield return null;
    }
}
