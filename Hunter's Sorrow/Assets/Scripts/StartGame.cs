using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private GameObject startText = default;
    private bool active = true;
    private float m_timeSinceActive = 0.0f;

    void Start()
    {
        startText = transform.GetChild(1).gameObject;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
            SceneManager.LoadScene(1);

        m_timeSinceActive += Time.deltaTime;

        if ((active && m_timeSinceActive > 0.2) || (!active && m_timeSinceActive > 0.5))
        {
            startText.SetActive(active);
            active = !active;
            m_timeSinceActive = 0.0f;
        }

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }
}
