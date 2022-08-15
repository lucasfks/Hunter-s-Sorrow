using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            try
            {
                other.gameObject.GetComponent<Bandit>().Death();
            }
            catch { other.gameObject.GetComponent<Goblin>().Death(); }
        }
    }
}