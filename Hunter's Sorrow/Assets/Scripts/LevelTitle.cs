using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTitle : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    public GameObject reference;
    private float _relativePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _relativePosition = m_player.position.x - reference.position.x;
        
    }
}
