using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftingPlatform : MonoBehaviour
{
    Vector3 tempPos1;
    Vector3 tempPos2;
    public float maxPos;
    public float eachPos;
    private void Start()
    {
        tempPos1 = transform.position;
        tempPos2 = new Vector3(transform.position.x,transform.position.y + maxPos,0);
    }
    private void Update()
    {
        float distance = Mathf.Abs(transform.position.y - tempPos1.y);

        transform.position = Vector3.MoveTowards(transform.position,tempPos2,eachPos*Time.deltaTime);
        Debug.Log(distance);
        if(distance >= maxPos)
        {
            Vector3 temp = tempPos1;
            tempPos1 = tempPos2;
            tempPos2 = temp;
            Debug.Log("1");
        }
    }
}
