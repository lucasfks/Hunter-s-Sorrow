using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTitle : MonoBehaviour
{
    private float _timer = 0f;
    private GameObject title = default;

    void Start()
    {
        title = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= 3f)
        {
            title.SetActive(false);
        }
    }
}