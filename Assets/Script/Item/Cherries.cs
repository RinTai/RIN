using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cherries : MonoBehaviour
{
    public GameObject player;
    public int addHpPerThreeSeconds;//每三秒恢复的血量
    private bool isStart = false;//是否开始
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

    IEnumerator TimeRecord()//携程记录攻击时间
    {
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //血量增加
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //血量增加
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //血量增加
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        player.GetComponentInChildren<HpControl>().hp += addHpPerThreeSeconds; //血量增加
        if (player.GetComponentInChildren<HpControl>().hp > player.GetComponentInChildren<HpControl>().maxHp)
        {
            player.GetComponentInChildren<HpControl>().hp = player.GetComponentInChildren<HpControl>().maxHp;
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
