using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float speed = 12f;//�ӵ�������ٶ�
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,4f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, -Time.deltaTime * speed, 0);  //�ӵ�λ��
    }
}
