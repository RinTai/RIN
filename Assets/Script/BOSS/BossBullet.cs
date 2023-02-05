using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 7f;//�ӵ�������ٶ�
    [SerializeField] private Transform skadiCorrupt;

    void Start()
    {
        skadiCorrupt = GameObject.FindGameObjectWithTag("SkadiCorruptingHeart").GetComponent<Transform>();//��ȡ����λ����Ϣ
        Destroy(gameObject, 7f);  //7s����������
    }

    // Update is called once per frame
    void Update()
    {
        if(skadiCorrupt.localScale.x >= 0)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);  //�ӵ�λ��
        }
        else
        {
            transform.Translate(-Time.deltaTime * speed, 0, 0);  //�ӵ�λ��          
        }
        
    }
}
