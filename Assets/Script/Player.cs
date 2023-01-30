using System.Collections;
using System.Collections.Generic;
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
    bool isattack = false;
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
    //����ΪԶ�̵��˹��������ʱ����ܻ��󳷵Ĺ���ʵ��
    public Transform player_0;//��ȡ��ҵ�λ�����
    public Transform farAttackEnemy;//��ȡ���˵�λ�����
    private float BeattackTime = 0.2f;//�ܻ�ʱ��
    private float beattackcd = 2f;//���������к������ʱ��
    public Animator anim;//��̬�������
    public GameObject N_hitbox_1, N_hitbox_2, N_hitbox_3;
    bool beattack = false;
    int direction_e_p = 0;
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
    }
    void Update()
    {
        transfor = this.gameObject.transform.position;
        PlayerMove();
        if (!beattack)
        {
            if (!isdash)//����ж���������д0.0
            {
                PlayerJumpByTwice();
                MoveObject();
                if (Input.GetKeyDown(KeyCode.J))//����
                {
                    isattack = true;
                    anim.SetBool("PrepareAttack", true);
                    Debug.Log("succeesful attack");
                }
                //���ڹ���
                NormalAttack();
                Debug.Log(isattack);
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
                GetComponent<SpriteRenderer>().color = Color.white;
                anim.SetInteger("MOVE", 0);
            }
        }
    }
    private void FixedUpdate()//�̶�Ϊÿ��50�μ��Ĺ̶��������
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);//������
    }
    public void MoveObject()
    {       
        inputpos = rb.velocity;
        inputpos.x = Input.GetAxisRaw("Horizontal") * speed;
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
    private void OnCollisionEnter2D(Collision2D collision)//��ҵ��ܻ�����������������������������������������������������������������������������������
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "Bullet")// ��ɫ�ܻ�����������
        {
            beattack = true;
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x >= 0)
                direction_e_p = -1;
            if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 0)
                direction_e_p = 1;
            GetComponent<SpriteRenderer>().color = Color.red;
            this.GetComponentInChildren<HpControl>().hp -= 25; //Ѫ������
            if(collision.gameObject.tag == "Bullet")
            {
                Instantiate(attackEffect, transform.position, Quaternion.identity);//���ɹ�����Ч
                Debug.Log("Attack");
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
                this.gameObject.layer = 6;
                isdash = false;
                Wantdash = false;           
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
        }
        if (isJump)
        {
            jumpCount--;//�����е����⣬��һ����Ծʱ�޷���⵽��Ծ��������ʱ���ܼ�⵽�����Ҫʹ�ö����������Ļ�ֱ����������ϵ��ö����������Ϳ�����
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }
    }
    void Hitbox_use(GameObject hitbox)
    {
        hitbox.SetActive(true);
    }
    void Hitbox_clear(GameObject hitbox)
    {
        hitbox.SetActive(false);
    }
    void NormalAttack()//����
    {
        switch (anim.GetFloat("ATTACK"))
        {
            case 1:
                {
                    Hitbox_clear(N_hitbox_3);
                    Hitbox_use(N_hitbox_1);
                    break;
                }
            case 2:
                {
                    Hitbox_clear(N_hitbox_1);
                    Hitbox_use(N_hitbox_2);
                    break;
                }
            case 3:
                {
                    Hitbox_clear(N_hitbox_1);
                    Hitbox_clear(N_hitbox_2);
                    Hitbox_use(N_hitbox_3);
                    break;
                }
            default:
                isattack = false;
                Hitbox_clear(N_hitbox_3);
                Hitbox_clear(N_hitbox_2);
                Hitbox_clear(N_hitbox_1);
                break;

        }
    }
    /*public void beAttackedByBullet()//����ܵ��ӵ��Ĺ���
    {
        beattack = true;
        GetComponent<SpriteRenderer>().color = Color.red;
        if (player_0.position.x >= farAttackEnemy.position.x)
        {
            Debug.Log("���ҷ�");
            rb.AddForce(new Vector2(200, 5), ForceMode2D.Force);
        }
        else if (player_0.position.x < farAttackEnemy.position.x)
        {
            Debug.Log("�����");
            rb.AddForce(new Vector2(-200, 5), ForceMode2D.Force);
        }*/
    
    public void PlayerMove()
    {
        if (rb.velocity.x == 0)
            anim.SetInteger("MOVE", 0);
        if (!isdash && rb.velocity.x != 0)
        {
            anim.SetInteger("MOVE", 1);
            Debug.Log("IS MOVING");
        }
        if (isdash)
            anim.SetInteger("MOVE", 2);
        if (beattack)
            anim.SetInteger("MOVE", 2);

    }
    public void PlayerBeAttacking()//����������
    {

    }
}
   
