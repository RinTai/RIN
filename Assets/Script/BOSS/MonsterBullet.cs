using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float speed = 12f;//�ӵ�������ٶ�
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,7f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -Time.deltaTime * speed, 0);  //�ӵ�λ��
    }
}
