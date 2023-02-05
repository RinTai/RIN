using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float speed = 7f;//子弹发射的速度
    [SerializeField] private Transform skadiCorrupt;

    void Start()
    {
        skadiCorrupt = GameObject.FindGameObjectWithTag("SkadiCorruptingHeart").GetComponent<Transform>();//获取敌人位置信息
        Destroy(gameObject, 7f);  //7s后销毁自身
    }

    // Update is called once per frame
    void Update()
    {
        if(skadiCorrupt.localScale.x >= 0)
        {
            transform.Translate(Time.deltaTime * speed, 0, 0);  //子弹位移
        }
        else
        {
            transform.Translate(-Time.deltaTime * speed, 0, 0);  //子弹位移          
        }
        
    }
}
