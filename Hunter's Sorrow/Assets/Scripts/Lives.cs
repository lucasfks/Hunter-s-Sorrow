using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    private GameObject heart1 = default;
    private GameObject heart2 = default;
    private GameObject heart3 = default;
    private GameObject heart4 = default;
    private GameObject heart5 = default;
    [SerializeField] GameObject m_player;

    void Start()
    {
        heart1 = transform.GetChild(0).gameObject;
        heart2 = transform.GetChild(1).gameObject;
        heart3 = transform.GetChild(2).gameObject;
        heart4 = transform.GetChild(3).gameObject;
        heart5 = transform.GetChild(4).gameObject;
    }

    void Update()
    {
        int life = m_player.GetComponent<HeroKnight>().GetLife();
        if(life >= 5)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(true);
        }else if (life == 4)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(true);
            heart5.SetActive(false);
        }
        else if (life == 3)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(true);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        else if (life == 2)
        {
            heart1.SetActive(true);
            heart2.SetActive(true);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        else if (life == 1)
        {
            heart1.SetActive(true);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
        else if (life <= 0)
        {
            heart1.SetActive(false);
            heart2.SetActive(false);
            heart3.SetActive(false);
            heart4.SetActive(false);
            heart5.SetActive(false);
        }
    }
}