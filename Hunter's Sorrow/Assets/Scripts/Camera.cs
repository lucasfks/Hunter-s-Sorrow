using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;
    private float positionY;

    void Start() {
        positionY = player.position.y + offset.y;
    }
    
    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, positionY, offset.z);
    }
}