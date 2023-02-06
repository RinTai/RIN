using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformUpToDown : MonoBehaviour
{
    PlatformEffector2D platform;
    void Start()
    {
        platform = GetComponent<PlatformEffector2D>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            platform.rotationalOffset = 180f;
        }
        else
        {
            platform.rotationalOffset = 0f;
        }
    }

    
}
