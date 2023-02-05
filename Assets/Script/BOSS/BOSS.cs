using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    Rigidbody2D Boss;
    RaycastHit2D[] Close;
    RaycastHit2D[] Far;
    bool isFar, isNear;
    Vector2 BossDic;
    Transform player;
    float WalkSpeed = 3f;
    Vector2 Temp;//�����ı�boss����;
    bool isAttack = false;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null)
        player=GameObject.FindGameObjectWithTag("Player").transform;//�����λ��
        Boss = GetComponent<Rigidbody2D>();
        BossDicChange();
        BossDicJug();
        if (!isAttack)
        {
            BossMove();
        }
        else
        {
            if (isNear)
            {

            }
            if (isFar)
            {

            }
        }



    }
    void RaycastClose()//��ս�ж�
    {
        Close = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), BossDic, 0.5f);
        if(Close != null)
            isNear = true;
        else
            isNear = false;
    }
    void RaycastFar()
    {
        Far = Physics2D.RaycastAll(new Vector2(transform.position.x, transform.position.y), BossDic, 2f);
        if(Far != null)
            isFar = true;
        else
            isFar = false;
    }

    void BossDicJug()//�õ�Boss�ķ���
    {
        if (Boss.transform.localScale.x >= 0)
            BossDic = new Vector2(1, 0 );
        else
            BossDic = new Vector2(-1, 0);
    }
    void BossDicChange()//Boss����ĸı��ж�
    {
            Temp = new Vector2(0, Boss.transform.localScale.y) ;
        if(Boss.velocity.x >= 0)
            Temp.x = Mathf.Abs(Boss.transform.localScale.x);
        if (Boss.velocity.x < 0)
            Temp.x = -Mathf.Abs(Boss.transform.localScale.x);
        Boss.transform.localScale = Temp;
    }
    void BossMove()//Boss���ƶ�
    {
        if (player.position.x <= Boss.position.x)
            Boss.velocity = new Vector2 (-WalkSpeed, Boss.velocity.y);
        else
            Boss.velocity = new Vector2(WalkSpeed, Boss.velocity.y);
    }
}
