using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip sliceSound;

    void Start() 
    {
        audioSource.clip = sliceSound;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Bandit>().Death();
            audioSource.Play();
        }
        else if (other.CompareTag("BringerOfDeath"))
        {
            other.gameObject.GetComponent<BringerOfDeath>().Hit();
            audioSource.Play();
        }
        else if (other.CompareTag("EvilWizard"))
        {
            other.gameObject.GetComponent<EvilWizard>().Hit();
            audioSource.Play();
        }
    }
}