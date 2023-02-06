using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    //Animator anim;
    PlatformEffector2D door;
    void Start()
    {
        //anim = GetComponent<Animator>();
        door= GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //anim.SetTrigger("open");
            door.surfaceArc = 0f;
        }
    }
}
