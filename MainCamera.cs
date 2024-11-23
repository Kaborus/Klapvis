using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
    }

    private void Update()
    {
        if (player != null)
        {
            transform.position = player.transform.position + new Vector3(0, 0, -10);
        }
    }
}
