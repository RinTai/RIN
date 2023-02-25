using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cherries : MonoBehaviour
{
    public GameObject player;
    public int addHpPerThreeSeconds;//ÿ����ָ���Ѫ��
    private bool isStart = false;//�Ƿ�ʼ
    public GameObject cherries;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cherries = GameObject.FindGameObjectWithTag("Cherries");
    }
    public void PerFiveSecondsHeal()
    {       
        StartCoroutine(TimeRecord());           
    }

    IEnumerator TimeRecord()//Я�̼�¼����ʱ��
    {
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //Ѫ������
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //Ѫ������
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //Ѫ������
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //Ѫ������
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
