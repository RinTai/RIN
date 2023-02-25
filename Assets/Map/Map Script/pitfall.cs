using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pitfall : MonoBehaviour
{
    EnemyPatrol enemy;

    public GameObject bullet;
    public Transform shotPlace;

    private float fireTime = 2f;
    private float lastFireTime = 0.0f;

    void Start()
    {
        
    }

    
    void Update()
    {
        lastFireTime += Time.deltaTime;
        if(lastFireTime>=fireTime)
        {
            Instantiate(bullet,shotPlace.position,transform.rotation);
            lastFireTime = 0;
        }
    }
}
