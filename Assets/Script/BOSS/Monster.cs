using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //以下为海嗣的物理组件
    private Rigidbody2D rb;
    //以下为海嗣的动画机
    public Animator animator;//角色的状态机
    //以下为海嗣攻击的计时器与记录次数
    public float timer;
    public int count;
    public bool isDisappear = false;
    //以下为海嗣子弹
    public Transform bulletPosition1;
    public Transform bulletPosition2;
    public Transform bulletPosition3;
    public Transform bulletPosition4;
    public Transform bulletPosition5;
    public Transform bulletPosition6;
    public Transform bulletPosition7;
    public Transform bulletPosition8;
    public Transform bulletPosition9;
    public GameObject monsterBullet;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(count <= 2)
        {
            if (timer > 5)
            {
                animator.SetBool("IsAttack", true);
                timer = 0;
                count++;
            }
            else
            {
                animator.SetBool("IsAttack", false);
            }
        }
        else
        {
            animator.SetBool("IsDie", true);         
        }
    }
    private void Fire()
    {
        Instantiate(monsterBullet, bulletPosition1.position, transform.rotation);//生成子弹
        Instantiate(monsterBullet, bulletPosition2.position, transform.rotation);
        Instantiate(monsterBullet, bulletPosition3.position, transform.rotation);
        Instantiate(monsterBullet, bulletPosition4.position, transform.rotation);
        Instantiate(monsterBullet, bulletPosition5.position, transform.rotation);
        Instantiate(monsterBullet, bulletPosition6.position, transform.rotation);
        Instantiate(monsterBullet, bulletPosition7.position, transform.rotation);
        Instantiate(monsterBullet, bulletPosition8.position, transform.rotation);
        Instantiate(monsterBullet, bulletPosition9.position, transform.rotation);
    }
    private void Distroy()
    {
        Destroy(gameObject);
    }
}
