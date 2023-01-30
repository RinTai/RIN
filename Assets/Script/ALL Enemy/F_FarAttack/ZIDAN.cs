using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ZIDAN : MonoBehaviour
{
    public F_E_FarAttack_AI FE;
    Rigidbody2D RB;
    Vector2 tttempposition;
    float moveSpeed = 2f;
    bool Awaken=true;
    int n = 3;
    // Start is called before the first frame update
    void Start()
    {
        RB =GetComponent<Rigidbody2D>(); 
    }
    private void Awake()
    {     
        tttempposition = FE.tempposition;
    }

    // Update is called once per frame
    void Update()
    {
     if(n<=0)
        {
            RB.velocity = FE.Enemy.velocity;
           this.transform.position = FE.fireposition.position;
            n = 3;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player"||collision.gameObject.tag=="Ground")
        {
            n--;
        }
    }
}
