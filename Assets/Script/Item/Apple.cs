using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    public GameObject player;
    public int addHp;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void HealPlayer()//�ָ�100Ѫ��
    {
        if (player.GetComponentInChildren<HpControl>().hp < player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp += addHp; //Ѫ�����ӡ�
            if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
            {
                player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
            }
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
