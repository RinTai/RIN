using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpToDown : MonoBehaviour
{
    PlatformEffector2D platform;

    private bool isDown = false;
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.S) && isDown)
        {
            platform.rotationalOffset = 180f;
        }
        else
        {
            platform.rotationalOffset = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDown= true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDown= false;
        }
    }
}
