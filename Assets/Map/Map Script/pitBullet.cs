using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitBullet : MonoBehaviour
{
    public float lifeTime;
    public float lifeMaxTime;
    public float speed;
    public float moveMax;

    public GameObject destroyEffect;//�ӵ����ٵ���Ч
    public GameObject attackEffect;//��������ҵ���Ч

    public bool isUp;
    public bool isDown;
    public bool isLeft;
    public bool isRight;

    void Start()
    {
        
    }

    
    void Update()
    {
        lifeTime += Time.deltaTime;
        if( isLeft )
        {
            Vector3 temp = new Vector3(transform.position.x - moveMax, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);
        }else if( isRight )
        {
            Vector3 temp = new Vector3(transform.position.x + moveMax, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);
        }else if(isUp)
        {
            Vector3 temp = new Vector3(transform.position.x, transform.position.y + moveMax, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);
        }else if(isDown)
        {
            Vector3 temp = new Vector3(transform.position.x, transform.position.y - moveMax, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, temp, speed * Time.deltaTime);
        }
        
        if(lifeTime >= lifeMaxTime)
        {
            OnDestroy();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ground")
        {
            OnDestroy();
        }
    }
    private void OnDestroy()
    {
        Instantiate(destroyEffect, transform.position, transform.rotation);//������Ч
        Destroy(gameObject);//�����ӵ�
    }
}
