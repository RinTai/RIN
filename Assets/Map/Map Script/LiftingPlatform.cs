using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftingPlatform : MonoBehaviour
{
    Vector3 tempPos1;//��ʼλ��
    Vector3 tempPos2;//����λ��
    public float maxPos;//λ�Ʋ�
    public float eachPos;//ÿ���ƶ�����
    private void Start()
    {
        tempPos1 = transform.position;
        tempPos2 = new Vector3(transform.position.x,transform.position.y + maxPos,0);
    }
    private void Update()
    {
        float distance = Mathf.Abs(transform.position.y - tempPos1.y);

        transform.position = Vector3.MoveTowards(transform.position,tempPos2,eachPos*Time.deltaTime);
        
        if(distance >= maxPos)//����λ�����ʼλ�õĽ���
        {
            Vector3 temp = tempPos1;
            tempPos1 = tempPos2;
            tempPos2 = temp;
            
        }
    }
}
