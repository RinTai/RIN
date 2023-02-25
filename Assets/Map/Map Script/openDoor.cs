using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDoor : MonoBehaviour
{
    Animator anim;
    PlatformEffector2D door;
    bool isOpen = false;
    bool isPlayer = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        door= GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && isPlayer)
        {
            isOpen = true;
            door.surfaceArc = 0f;
        }
        else
        {
            isOpen = false;
            door.surfaceArc = 360f;
        }
        if (isOpen)
        {
            anim.SetTrigger("open");
        }
        else
        {
            anim.SetTrigger("close");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayer= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayer= false;
        }
    }
}
