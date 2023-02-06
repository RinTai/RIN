using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS2Skill : MonoBehaviour
{
    //以下为敌人启用动画时的动画机
    public Animator animator;
    //以下为子弹及其发射角度
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    public List<BulletCharacter> tempBullets;

    public int shotCount;//发射次数
    public int rotateBulletAngle;//子弹间隔的角度
    public int rotateBulletCount;//一次发射子弹的总数
    //注意要让子弹角度与总数之积为360°

    public BOSS2 boss2;//调用变量

    public void SkillAnimation()//开启技能
    {
        animator.SetBool("IsSkill", true);
    }

    public void FireRound()//圆形弹幕
    {
        StopAllCoroutines();
        ClearBulletsList();
        StartCoroutine(FirRound(shotCount, firPoint.transform.position));
    }

    public void FireRoundGroup()//密集型弹幕
    {
        StopAllCoroutines();
        ClearBulletsList();
        StartCoroutine(FirRoundGroup());
    }
    public void FireTurbine()//涡轮弹幕
    {
        StopAllCoroutines();
        ClearBulletsList();
        StartCoroutine(FirTurbine());
    }

    IEnumerator FirRound(int number, Vector3 creatPoint)//圆形弹幕
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(rotateBulletAngle, Vector3.forward);//使用四元数制造绕Z轴旋转10度的旋转
        for (int i = 0; i < number; i++)    //发射波数
        {
            for (int j = 0; j < rotateBulletCount; j++)//一共发射rotateBulletCount个子弹
            {
                CreatBullet(bulletDir, creatPoint);
                bulletDir = rotateQuate * bulletDir; //让发射方向旋转rotateBulletAngle度，到达下一个发射方向
            }
            yield return new WaitForSeconds(0.5f); //协程延时，0.5秒进行下一波发射
        }
        yield return null;
    }

    IEnumerator FirRoundGroup()//组合圆形弹幕
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);//使用四元数制造绕Z轴旋转45度的旋转
        List<BulletCharacter> bullets = new List<BulletCharacter>();       //装入开始生成的8个弹幕
        for (int i = 0; i < 8; i++)
        {
            var tempBullet = CreatBullet(bulletDir, firPoint.transform.position);
            bulletDir = rotateQuate * bulletDir; //生成新的子弹后，让发射方向旋转45度，到达下一个发射方向
            bullets.Add(tempBullet);
        }
        yield return new WaitForSeconds(1.0f);   //1秒后在生成多波弹幕
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].speed = 0; //弹幕停止移动
            StartCoroutine(FirRound(6, bullets[i].transform.position));//通过之前弹幕的位置，生成多波多方向的圆形弹幕
        }
    }
    IEnumerator FirTurbine()//涡轮形
    {
        Vector3 bulletDir = firPoint.transform.up;      //发射方向
        Quaternion rotateQuate = Quaternion.AngleAxis(20, Vector3.forward);//使用四元数制造绕Z轴旋转20度的旋转
        float radius = 0.6f;        //生成半径
        float distance = 0.2f;      //每生成一次增加的距离
        for (int i = 0; i < 18; i++)
        {
            Vector3 firePoint = firPoint.transform.position + bulletDir * radius;   //使用向量计算生成位置
            StartCoroutine(FirRound(1, firePoint));     //在算好的位置生成一波圆形弹幕
            yield return new WaitForSeconds(0.05f);      //延时较小的时间（为了表现效果），计算下一步
            bulletDir = rotateQuate * bulletDir;        //发射方向改变
            radius += distance;     //生成半径增加
        }
    }
    public BulletCharacter CreatBullet(Vector3 dir, Vector3 creatPoint)//发射子弹
    {
        BulletCharacter bulletCharacter = Instantiate(bulletTemplate, creatPoint, Quaternion.identity);
        bulletCharacter.gameObject.SetActive(true);
        bulletCharacter.dir = dir;
        tempBullets.Add(bulletCharacter);
        return bulletCharacter;
    }
    public void ClearBulletsList()//清空子弹
    {
        if (tempBullets.Count > 0)
        {
            for (int i = (tempBullets.Count - 1); i >= 0; i--)
            {
                Destroy(tempBullets[i].gameObject);
            }
        }

        tempBullets.Clear();

    }
    public void StopAnimation()
    {
        animator.SetBool("IsSkill", false);//停止动画
        animator.SetBool("IsChangePosition", true);//停止动画     
    }//停止动画
    public void moveTime0()
    {
        boss2.moveCount = 0;
    }//移动时间归零防止bug发生
}
