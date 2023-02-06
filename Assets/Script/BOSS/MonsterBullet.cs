using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float speed = 12f;//子弹发射的速度
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,4f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(0, -Time.deltaTime * speed, 0);  //子弹位移
    }
}
