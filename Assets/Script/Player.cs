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
    bool isdash=false;//�Ƿ���
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
    public Animator anim;//��̬�������
    public GameObject N_hitbox_1,N_hitbox_2,N_hitbox_3;
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
            if (Input.GetKeyDown(KeyCode.J) && anim.GetBool("isAttack") == false)
            {
                anim.SetBool("PrepareAttack", true);
                Debug.Log("succeesful attack");
                StartCoroutine(NormalAttack());
            }
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
                this.gameObject.layer =9;
                rb.velocity = new Vector2(dashspeed * Dashdirection, 0);
            }           
    }
    void Dashready()
    {
        isdash = true;
        dashtimeleft = dashtime;
        dashLast = Time.time;
    }
    /*void NormalAttack()
    {
        anim.SetBool("isAttack",true);
        Hitbox_use(N_hitbox_1);
 
        if (Input.GetKey(KeyCode.J) && anim.GetBool("isAttack"))
        {
            anim.SetBool("isAttack_1", true);
            Hitbox_use(N_hitbox_2);
        
            if (Input.GetKey(KeyCode.J) && anim.GetBool("isAttack_1"))
            {
                anim.SetBool("isAttack_2", true );
                Hitbox_use(N_hitbox_3);
                anim.SetBool("isAttack_2", false);
              
            }
            anim.SetBool("isAttack_1", false);
        }      
        anim.SetBool("isAttack", false);
    }*/

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
            jumpCount--;
            rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpCount--;
            isJump = false;
        }
    }
    void Hitbox_use(GameObject hitbox)
    {
        hitbox.SetActive(true);
       // hitbox.SetActive(false);
    }
    IEnumerator NormalAttack()
    {
        for (int i= 0;i<3;i++)
        {
            if (anim.GetBool("WantAttack_3") || anim.GetBool("PrepareAttack"))
            {
                Hitbox_use(N_hitbox_1);
                continue;
            }
        if(anim.GetBool("WantAttack_1"))
            {
                Hitbox_use(N_hitbox_2);
                continue;
            }
        if(anim.GetBool("WantAttack_2"))
            {
                Hitbox_use(N_hitbox_3);
                continue;
            }
            if (!anim.GetBool("WantAttck_1") && !anim.GetBool("WantAttack_2") && !anim.GetBool("WantAttack_3") && anim.GetBool("PrepareAttack"))
                break;               
            yield return new WaitForSeconds(0.75f);
        }
        
            
      

    }
}
