using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Bandit>().Death();
        }
        else if (other.CompareTag("BringerOfDeath"))
        {
            other.gameObject.GetComponent<BringerOfDeath>().Hit();
        }
        else if (other.CompareTag("EvilWizard"))
        {
            other.gameObject.GetComponent<EvilWizard>().Hit();
        }
    }
}