using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_E_FarAttackRange : MonoBehaviour
{
    public F_E_FarAttack_AI F_E_Ai;
    // Start is called before the first frame update
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