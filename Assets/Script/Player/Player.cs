using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;
    Rigidbody2D rb;
    public float speed = 5f;
    public float h = 5;
    public float g = 10f;
    float v;
    Vector2 inputpos;
    public static Vector2 transfor;
    Vector3 Playertran;
    bool isdash = false;//�Ƿ���
    float dashtime = 0.2f;
    float dashtimeleft, dashCD = 1f, dashLast = -10f;
    float dashspeed = 40f;
    int Dashdirection;
    float Force = 5f;
    bool Wantdash = false;
    bool isAttack = false;
    //����Ϊ��Ծ���
    [Range(1, 10)]
    private float jumpSpeed = 8f;
    private bool moveJump;//�ж��Ƿ�����Ծ
    private bool isGround;//�ж��Ƿ��ڵ�����
    public Transform groundCheck;//������
    public LayerMask ground;
    //����Ϊ��Ծ�Ż�
    public float fallMultiplier = 1000f;//����������
    public float lowJumpMultiplier = 600f;//С��������
    public float fallMultiplierElse = 3f;//��������
    //����Ϊ��������ܵ�ʵ��
    public int jumpCount = 2;//��Ծ����
    private bool isJump;//��ʾ��Ծ״̬
    //����Ϊ�ӵ���Ч
    public GameObject destroyEffect;//�ӵ����ٵ���Ч
    public GameObject attackEffect;//��������ҵ���Ч
    //����Ϊ�ܻ����޵�ʱ��
    public float flashes;//��˸����
    public float duration;//��˸����
    private SpriteRenderer sr;//��ɫ
    //����ΪԶ�̵��˹��������ʱ����ܻ��󳷵Ĺ���ʵ��
    public Transform player_0;//��ȡ��ҵ�λ�����
    public Transform farAttackEnemy;//��ȡ���˵�λ�����
    private float BeattackTime = 0.2f;//�ܻ�ʱ��
    private float beattackcd = 2f;//���������к������ʱ��
    public GameObject N_hitbox_1, N_hitbox_2, N_hitbox_3;
    bool beattack = false;
    int direction_e_p = 0;
    //����Ϊ��ɫ����
    public Animator animator;//��ɫ��״̬��
    private void Awake()
    {
        player = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        v = Mathf.Sqrt(2 * h / g);
        rb = gameObject.GetComponent<Rigidbody2D>();
        inputpos = new Vector2();
        Playertran = rb.transform.localScale;
        sr = gameObject.GetComponent<SpriteRenderer>();//��ɫ���
    }
    void Update()
    {
        transfor = this.gameObject.transform.position;
        if (!beattack)
        {
            if (!isdash)//����ж���������д0.0
            {
                PlayerJumpByTwice();
                MoveObject();
                if (Input.GetKeyDown(KeyCode.J))//����
                {
                    isAttack = true;
                    NormalAttack();//���ڹ���                  
                }
                else
                {
                    isAttack = false;
                    animator.SetBool("IsAttack", false);//����������ת��
                }             
            }
            if (Input.GetKeyDown(KeyCode.L))
                if (Time.time >= (dashLast + dashCD))
                    Wantdash = true;
            if (Wantdash)
                Dash();
        }
        if (beattack)
        {
            if (BeattackTime == 0.2f)
                PlayerBeAttacking();
            BeattackTime -= Time.deltaTime;
            if (BeattackTime <= 0)
            {
                beattack = false;
                BeattackTime = 0.2f;
            }
        }
    }
    private void FixedUpdate()//�̶�Ϊÿ��50�μ��Ĺ̶��������
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);//������
    }
    public void MoveObject()//�����ҵĳ���Ļ����ƶ�
    {       
        inputpos = rb.velocity;
        inputpos.x = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("SpeedX",Mathf.Abs(inputpos.x));//���߶�����ת��
        rb.velocity = inputpos;
        if (inputpos.x < 0)
        {
            Playertran.x = -Mathf.Abs(Playertran.x);
            Dashdirection = -1;
        }
        if (inputpos.x > 0)
        {
            Playertran.x = Mathf.Abs(Playertran.x);
            Dashdirection = 1;
        }
        rb.transform.localScale = Playertran;
    }
    private void OnCollisionEnter2D(Collision2D collision)//��ҵ��ܻ�
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Bullet")// ��ɫ�ܻ�����������
        {
            beattack = true;
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x >= 0)
                direction_e_p = -1;
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 0)
                direction_e_p = 1;
            //GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(BeAttackedInvincibleTime());//�����ܻ���˸��Я��
            this.GetComponentInChildren<HpControl>().hp -= 25; //Ѫ������
            if(collision.gameObject.tag == "Bullet")
            {
                Instantiate(attackEffect, transform.position, Quaternion.identity);//���ɹ�����Ч                
                Destroy(collision.gameObject);//�����ӵ�
            }
            rb.AddForce(new Vector2(direction_e_p * Force, 2f), ForceMode2D.Impulse);
        }

    }
    void Dash()
    {
        {
            if (Time.time >= (dashLast + dashCD))
            {
                Dashready();
            }
            Rush();

        }
        if (isdash)
        {
            dashtimeleft -= Time.deltaTime * 2;
            if (dashtimeleft < 0)
            {
                isdash = false;
                Wantdash = false;
                this.gameObject.layer = 6;
            }
        }
    }
    void Rush()
    {
        if (isdash)
            if (dashtimeleft >= 0)
            //transform.Translate(transform.right * Time.deltaTime * dashtime*Dashdirection*dashspeed);
            {
                this.gameObject.layer = 9;
                rb.velocity = new Vector2(dashspeed * Dashdirection, 0);
            }
    }
    void Dashready()
    {
        isdash = true;
        dashtimeleft = dashtime;
        dashLast = Time.time;
    }
    void PlayerJumpByTwice()//������
    {
        moveJump = Input.GetButtonDown("Jump");
        animator.SetFloat("SpeedY", inputpos.y);//��Ծ������ת��
        animator.SetBool("IsJump", true);//��Ծ������ת��
        JumpDetectionByTwice();
        //���Ƿֽ��ߣ�����Ϊ�Ż���Ծ�ָ�����
        if (Input.GetButtonDown("Jump") && rb.velocity.y < 0 && jumpCount > 0)
        {
            rb.velocity = Vector2.up * 7;
        }
        else if (rb.velocity.y < 0 && !Input.GetButtonDown("Jump"))
        {
            if (jumpCount > 0) rb.velocity += Vector2.up * Physics2D.gravity.y * fallMultiplier * Time.deltaTime;
            if (jumpCount == 0) rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.gravityScale = lowJumpMultiplier;
        }
        else
        {
            rb.gravityScale = 1.5f;
        }
    }
    void JumpDetectionByTwice()//���������
    {
        if (moveJump && jumpCount > 0)
        {
            isJump = true;
        }
        if (isGround)//�ж��Ƿ��ڵ���
        {
            jumpCount = (int)2f;//��������Ϊ2
            animator.SetBool("IsJump", false);//��Ծ������ת��
        }
        if (isJump)
        {
            jumpCount--;//�����е����⣬��һ����Ծʱ�޷���⵽��Ծ��������ʱ���ܼ�⵽�����Ҫʹ�ö����������Ļ�ֱ����������ϵ��ö����������Ϳ�����
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;            
        }
    }
  
    public void PlayerBeAttacking()//����������
    {

    }

    IEnumerator BeAttackedInvincibleTime()//�ܵ���������޵�ʱ��
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);//�������ײʱ
        Physics2D.IgnoreLayerCollision(6, 10, true);//���ӵ���ײʱ
        sr.color = Color.red;
        yield return new WaitForSeconds(duration / flashes);
        for (int i = 0; i < flashes; i++)
        {
            sr.color = new Color(255 / 255, 255 / 255, 255 / 255, 0.6f);//͸���ȱ仯
            yield return new WaitForSeconds(duration / flashes);
            sr.color = Color.white;
            yield return new WaitForSeconds(duration / flashes);
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
        Physics2D.IgnoreLayerCollision(6, 10, false);
    }

    private void NormalAttack()//��ͨ����
    {
        animator.SetBool("IsAttack", true);//����������ת��
    }
}
   