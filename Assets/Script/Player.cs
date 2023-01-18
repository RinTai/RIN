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
    bool isdash;
    float dashtime=0.2f;
    float dashtimeleft,dashCD=1f,dashLast=-10f;
    float dashspeed=40f;
    int Dashdirection;
    float Force = 10f;
    bool Wantdash = false;
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
        if(!isdash)//����ж���������д0.0
        { 
            PlayerJumpByTwice();
            MoveObject();         
        }
        if (Input.GetKeyDown(KeyCode.L))
            if (Time.time >= (dashLast + dashCD))
                Wantdash =true;
        if(Wantdash)
            Dash();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int direction_e_p = 0;
        if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x >= 0)
            direction_e_p = -1;
        if (collision.gameObject.transform.position.x - this.gameObject.transform.position.x < 0)
            direction_e_p = 1;
        if (collision.gameObject.tag == "enemy")
        {
            rb.AddForce(new Vector2(direction_e_p * Force * 250, 200f), ForceMode2D.Force);
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
            }
        }
    }
    void Rush()
    {
        if (isdash)
            if (dashtimeleft >= 0)
            //transform.Translate(transform.right * Time.deltaTime * dashtime*Dashdirection*dashspeed);
            {
                rb.velocity = new Vector2(dashspeed * Dashdirection, 0);
            }           
    }
    void Dashready()
    {
        isdash = true;
        dashtimeleft = dashtime;
        dashLast = Time.time;
    }
    void Attack()
    {

    }

    void PlayerJumpByTwice()//������
    {
        moveJump = Input.GetButtonDown("Jump");
        JumpDetectionByTwice();
        //���Ƿֽ��ߣ�����Ϊ�Ż���Ծ�ָ�����
        if (Input.GetButtonDown("Jump") && rb.velocity.y < 0 && jumpCount > 0)
        {
            Debug.Log("111");
            rb.velocity = Vector2.up * 7;
        }
        else if (rb.velocity.y < 0 && !Input.GetButtonDown("Jump"))
        {
            Debug.Log("222");
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
            jumpCount--;
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }
    }
}
