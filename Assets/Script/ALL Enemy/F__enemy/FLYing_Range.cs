using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FLYing_Range : MonoBehaviour
{
    public F_E_Ai F_E_Ai;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            F_E_Ai.player0 = collision.GetComponent<Rigidbody2D>();
            F_E_Ai.attack = true;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            F_E_Ai.attack = false;
        }

    }
}
