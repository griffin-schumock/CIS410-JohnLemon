using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainRoom : MonoBehaviour
{
    public GameObject player;
    public GameObject rain;
    bool isRaining;

    void Start()
    {
        rain.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isRaining = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isRaining = false;
            rain.SetActive(false);
        }
    }

    void Rain()
    {
        rain.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaining)
        {
            Rain();
        }
    }
}
