using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BOSS2 : MonoBehaviour
{
    //以下为获得敌人的物理组件
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    //以下为获得玩家与Boss的位置信息
    private Transform player;
    private Vector3 boss2position;
    //以下为Boss瞬间移动的计时器
    [SerializeField] private float moveTimer;
    //以下为Boss的动画机
    public Animator animator;
    //以下为Boss进行两点间瞬间移动
    public Transform position1;
    public Transform position2;
    private Transform position;
    private bool isMove;
    //以下为瞬间移动
    private float speed = 10000;
    public float continueTime = 10;//停留时间
    public int moveCount = 0;//瞬移次数
    //以下为发射子弹
    public Transform normalFirePosition;//普通开火口
    public Transform firePosition1;//分裂子弹开火口
    public Transform firePosition2;
    public Transform firePosition3;
    public GameObject bullet;//普通子弹
    public GameObject bossbullet;//boss子弹
    public GameObject specialBullet;//特殊子弹
    //以下为召唤怪物
    public Transform monsterPosition;
    public GameObject monster;//召唤海嗣



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;//找玩家位置
        position = position1;
        boss2position = rb.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        moveTimer += Time.deltaTime;

        if (moveTimer / continueTime >= 1)
        {
            animator.SetBool("IsChangePosition", true);
            moveCount++;
            if(moveCount >= 4)
            {
                Skill1();
                animator.SetBool("IsSkill", true);               
            }
        }
 
    }
    private void SuddenMove()//瞬间移动判断
    {      
        isMove = true;
        if (position == position1)
        {            
            ChangeLocalScale();
            position = position2;
            transform.position = Vector2.MoveTowards(transform.position, position.position, speed * Time.deltaTime);//朝着目标点巡逻移动
            isMove = false;         
        }
        else if(position == position2)
        {
            ChangeLocalScale();
            position = position1;
            transform.position = Vector2.MoveTowards(transform.position, position.position, speed * Time.deltaTime);//朝着目标点巡逻移动
            isMove = false;                
        }
    }
    
    private void ChangeLocalScale()//改变敌人朝向
    {
        if(isMove)
        {
            Vector3 localTemp = rb.transform.localScale;
            localTemp.x *= -1;
            rb.transform.localScale = localTemp;
        }
    }
    private void CorrectTime()
    {       
        animator.SetBool("IsChangePosition", false);
    }
    private void Time0()
    {
        moveTimer = 0;
    }

    private void Shot()
    {
        Instantiate(bullet, normalFirePosition.position, transform.rotation);//生成子弹（锁定帧数120的情况下为发射一个子弹）
    }
    private void ShotSkill1()//分裂子弹
    {
        Instantiate(bossbullet, firePosition1.position, transform.rotation);//生成子弹（锁定帧数120的情况下为发射一个子弹）
        Instantiate(bossbullet, firePosition2.position, transform.rotation);//生成子弹（锁定帧数120的情况下为发射一个子弹）
        Instantiate(bossbullet, firePosition3.position, transform.rotation);//生成子弹（锁定帧数120的情况下为发射一个子弹）
    }
    private void ShotSkill2()//蓄力子弹
    {
        Instantiate(specialBullet, normalFirePosition.position, transform.rotation);//生成子弹（锁定帧数120的情况下为发射一个子弹）
    }

    private void Skill1()//召唤海嗣
    {
        Instantiate(monster, monsterPosition.position, transform.rotation);//生成海嗣
    }
}
