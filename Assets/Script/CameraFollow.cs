using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed = 5.0f;//��ȡ��������ٶ�
    public Transform target;//��ȡĿ��λ��
    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x+2, target.position.y+2, -10); 
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);//�������λ�ø��䱣��һ��
    }
}
