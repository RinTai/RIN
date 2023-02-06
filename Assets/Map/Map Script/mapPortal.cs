using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mapPortal : MonoBehaviour
{
    public GameObject target;
    private Transform playerPos;
    private bool isPortal = false;

    void Start()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
        beginPortal();
    }

    private void beginPortal()
    {
        if (isPortal)
        {
            playerPos.position = target.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPortal = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPortal = false;
        }
    }
}
