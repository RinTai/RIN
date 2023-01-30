using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class F_E_FarAttack_AI : MonoBehaviour
{
    public Rigidbody2D player0;
    public bool attack = false;
    public Rigidbody2D Enemy;
    float FlySpeed = 1.2f;
    float Tick = 1f;
    public LineRenderer line;
    float Tick2 = 4f;
    public Vector2 tempposition;
    public GameObject ZIDAN;
    public Transform fireposition;

    // Start is called before the first frame update
    void Start()
    {
        Enemy = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!attack)
        {
            line.enabled = false;
            Tick2 = 2f;
            Tick -= Time.deltaTime;
            if (Tick < 0)
            {
                float x = Random.Range(-2, 3);
                float y = Random.Range(-1, 2);
                Tick = 1f;
                Enemy.velocity = new Vector2(x * FlySpeed, y * FlySpeed * 0.1f);
            }
        }
        if (attack)
        {         
            Tick2 -= Time.deltaTime;
            Line();
            if (Tick2 <= 0)
            {
                Debug.Log("Attack");
                Attack();
                Tick2 = 2f;
            }
        }
    }
        public void Line()
        {
        Enemy.velocity = new Vector2(0f, 0f);
        if (Tick2 > 1f)
        {
            line.enabled = true;
            line.SetPosition(0, fireposition.position);
            line.SetPosition(1, player0.position);
            tempposition = player0.position;         
        }
        else
            line.enabled = false;
        }
        void Attack()
        {
        ZIDAN.GetComponent<Rigidbody2D>().velocity = new Vector2(-fireposition.position.x, -fireposition.position.y) + tempposition;
        }
    }

