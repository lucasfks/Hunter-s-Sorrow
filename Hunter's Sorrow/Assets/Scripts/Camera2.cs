using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera2 : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;


    void Update()
    {
        transform.position = new Vector3(player.position.x + offset.x, player.position.y + offset.y - 1, offset.z);
    }
}