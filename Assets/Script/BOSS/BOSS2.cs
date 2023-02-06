using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BOSS2 : MonoBehaviour
{
    //����Ϊ��õ��˵��������
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    //����Ϊ��������Boss��λ����Ϣ
    private Transform player;
    private Vector3 boss2position;
    //����ΪBoss˲���ƶ��ļ�ʱ��
    [SerializeField] private float moveTimer;
    //����ΪBoss�Ķ�����
    public Animator animator;
    //����ΪBoss���������˲���ƶ�
    public Transform position1;
    public Transform position2;
    private Transform position;
    private bool isMove;
    //����Ϊ˲���ƶ�
    private float speed = 10000;
    public float continueTime = 10;//ͣ��ʱ��
    public int moveCount = 0;//˲�ƴ���
    //����Ϊ�����ӵ�
    public Transform normalFirePosition;//��ͨ�����
    public Transform firePosition1;//�����ӵ������
    public Transform firePosition2;
    public Transform firePosition3;
    public GameObject bullet;//��ͨ�ӵ�
    public GameObject bossbullet;//boss�ӵ�
    public GameObject specialBullet;//�����ӵ�
    //����Ϊ�ٻ�����
    public Transform monsterPosition;
    public GameObject monster;//�ٻ�����



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;//�����λ��
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
    private void SuddenMove()//˲���ƶ��ж�
    {      
        isMove = true;
        if (position == position1)
        {            
            ChangeLocalScale();
            position = position2;
            transform.position = Vector2.MoveTowards(transform.position, position.position, speed * Time.deltaTime);//����Ŀ���Ѳ���ƶ�
            isMove = false;         
        }
        else if(position == position2)
        {
            ChangeLocalScale();
            position = position1;
            transform.position = Vector2.MoveTowards(transform.position, position.position, speed * Time.deltaTime);//����Ŀ���Ѳ���ƶ�
            isMove = false;                
        }
    }
    
    private void ChangeLocalScale()//�ı���˳���
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
        Instantiate(bullet, normalFirePosition.position, transform.rotation);//�����ӵ�������֡��120�������Ϊ����һ���ӵ���
    }
    private void ShotSkill1()//�����ӵ�
    {
        Instantiate(bossbullet, firePosition1.position, transform.rotation);//�����ӵ�������֡��120�������Ϊ����һ���ӵ���
        Instantiate(bossbullet, firePosition2.position, transform.rotation);//�����ӵ�������֡��120�������Ϊ����һ���ӵ���
        Instantiate(bossbullet, firePosition3.position, transform.rotation);//�����ӵ�������֡��120�������Ϊ����һ���ӵ���
    }
    private void ShotSkill2()//�����ӵ�
    {
        Instantiate(specialBullet, normalFirePosition.position, transform.rotation);//�����ӵ�������֡��120�������Ϊ����һ���ӵ���
    }

    private void Skill1()//�ٻ�����
    {
        Instantiate(monster, monsterPosition.position, transform.rotation);//���ɺ���
    }
}
