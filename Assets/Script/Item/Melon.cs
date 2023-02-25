using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melon : MonoBehaviour
{
    public GameObject player;
    public int addHp;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void HealPlayer ()//恢复50血量
    {   
        if(player.GetComponentInChildren<HpControl>().hp < player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp += addHp; //血量增加、
            if(player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
            {
                player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp ;
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
