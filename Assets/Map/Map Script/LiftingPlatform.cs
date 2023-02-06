using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftingPlatform : MonoBehaviour
{
    Vector3 tempPos1;//初始位置
    Vector3 tempPos2;//结束位置
    public float maxPos;//位移差
    public float eachPos;//每次移动距离
    private void Start()
    {
        tempPos1 = transform.position;
        tempPos2 = new Vector3(transform.position.x,transform.position.y + maxPos,0);
    }
    private void Update()
    {
        float distance = Mathf.Abs(transform.position.y - tempPos1.y);

        transform.position = Vector3.MoveTowards(transform.position,tempPos2,eachPos*Time.deltaTime);
        
        if(distance >= maxPos)//结束位置与初始位置的交互
        {
            Vector3 temp = tempPos1;
            tempPos1 = tempPos2;
            tempPos2 = temp;
            
        }
    }
}
