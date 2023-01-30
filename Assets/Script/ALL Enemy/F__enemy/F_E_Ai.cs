using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_E_Ai : MonoBehaviour
{
    public Rigidbody2D player0;
    public bool attack = false;
    Rigidbody2D Fe;
    float FlySpeed = 3;
    float RUSHspeed = 9f;
    float Tick = 1f;
    float AttackTime = 3f;
    int temp = 1;
    // Start is called before the first frame update
    void Start()
    {
        Fe = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attack)
        {
            Tick -= Time.deltaTime;
            if (Tick < 0)
            {          
                float x = Random.Range(-2, 3);            
                float y = Random.Range(-1, 2);
                Tick = 1f;
                Fe.velocity = new Vector2(x * FlySpeed, y * FlySpeed);
            }
        }
        if (attack)
        {
            Vector3 enemy_scale = Fe.transform.localScale;
            float ttemp = player0.position.x - Fe.position.x;
            if (ttemp > 0)
            {
                enemy_scale.x = Mathf.Abs(enemy_scale.x);
                temp = 1;
            }
            if (ttemp < 0)
            {
                enemy_scale.x = Mathf.Abs(enemy_scale.x) * -1;
                temp = -1;
            }
            Fe.transform.localScale = enemy_scale;
            AttackTime -= Time.deltaTime;
            if (AttackTime < 0)//¹¥»÷
            {
                AttackTime = 3f;
                Fe.velocity = (player0.position - Fe.position).normalized * RUSHspeed;
            }

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            Fe.velocity = new Vector2(-temp * 1.2f, 1.3f);
            Tick = 2f;
        }
        if( collision.gameObject.tag == "Ground")
        {
            Fe.velocity = new Vector2(temp * 1.2f, 2f);
            Tick = 2f;
        }
    }
}


