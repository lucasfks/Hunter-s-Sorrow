using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSpeech : MonoBehaviour
{
    private GameObject speech = default;
    void Start()
    {
        speech = transform.GetChild(0).gameObject;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            speech.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            speech.SetActive(false);
        }
    }

    void Update()
    {
    }
}
