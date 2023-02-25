using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS2Skill : MonoBehaviour
{
    //����Ϊ�������ö���ʱ�Ķ�����
    public Animator animator;
    //����Ϊ�ӵ����䷢��Ƕ�
    public BulletCharacter bulletTemplate;
    public Transform firPoint;
    public List<BulletCharacter> tempBullets;

    public int shotCount;//�������
    public int rotateBulletAngle;//�ӵ�����ĽǶ�
    public int rotateBulletCount;//һ�η����ӵ�������
    //ע��Ҫ���ӵ��Ƕ�������֮��Ϊ360��

    public BOSS2 boss2;//���ñ���

    public void SkillAnimation()//��������
    {
        animator.SetBool("IsSkill", true);
    }

    public void FireRound()//Բ�ε�Ļ
    {
        StopAllCoroutines();
        ClearBulletsList();
        StartCoroutine(FirRound(shotCount, firPoint.transform.position));
    }

    public void FireRoundGroup()//�ܼ��͵�Ļ
    {
        StopAllCoroutines();
        ClearBulletsList();
        StartCoroutine(FirRoundGroup());
    }
    public void FireTurbine()//���ֵ�Ļ
    {
        StopAllCoroutines();
        ClearBulletsList();
        StartCoroutine(FirTurbine());
    }

    IEnumerator FirRound(int number, Vector3 creatPoint)//Բ�ε�Ļ
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(rotateBulletAngle, Vector3.forward);//ʹ����Ԫ��������Z����ת10�ȵ���ת
        for (int i = 0; i < number; i++)    //���䲨��
        {
            for (int j = 0; j < rotateBulletCount; j++)//һ������rotateBulletCount���ӵ�
            {
                CreatBullet(bulletDir, creatPoint);
                bulletDir = rotateQuate * bulletDir; //�÷��䷽����תrotateBulletAngle�ȣ�������һ�����䷽��
            }
            yield return new WaitForSeconds(0.5f); //Э����ʱ��0.5�������һ������
        }
        yield return null;
    }

    IEnumerator FirRoundGroup()//���Բ�ε�Ļ
    {
        Vector3 bulletDir = firPoint.transform.up;
        Quaternion rotateQuate = Quaternion.AngleAxis(45, Vector3.forward);//ʹ����Ԫ��������Z����ת45�ȵ���ת
        List<BulletCharacter> bullets = new List<BulletCharacter>();       //װ�뿪ʼ���ɵ�8����Ļ
        for (int i = 0; i < 8; i++)
        {
            var tempBullet = CreatBullet(bulletDir, firPoint.transform.position);
            bulletDir = rotateQuate * bulletDir; //�����µ��ӵ����÷��䷽����ת45�ȣ�������һ�����䷽��
            bullets.Add(tempBullet);
        }
        yield return new WaitForSeconds(1.0f);   //1��������ɶನ��Ļ
        for (int i = 0; i < bullets.Count; i++)
        {
            bullets[i].speed = 0; //��Ļֹͣ�ƶ�
            StartCoroutine(FirRound(6, bullets[i].transform.position));//ͨ��֮ǰ��Ļ��λ�ã����ɶನ�෽���Բ�ε�Ļ
        }
    }
    IEnumerator FirTurbine()//������
    {
        Vector3 bulletDir = firPoint.transform.up;      //���䷽��
        Quaternion rotateQuate = Quaternion.AngleAxis(20, Vector3.forward);//ʹ����Ԫ��������Z����ת20�ȵ���ת
        float radius = 0.6f;        //���ɰ뾶
        float distance = 0.2f;      //ÿ����һ�����ӵľ���
        for (int i = 0; i < 18; i++)
        {
            Vector3 firePoint = firPoint.transform.position + bulletDir * radius;   //ʹ��������������λ��
            StartCoroutine(FirRound(1, firePoint));     //����õ�λ������һ��Բ�ε�Ļ
            yield return new WaitForSeconds(0.05f);      //��ʱ��С��ʱ�䣨Ϊ�˱���Ч������������һ��
            bulletDir = rotateQuate * bulletDir;        //���䷽��ı�
            radius += distance;     //���ɰ뾶����
        }
    }
    public BulletCharacter CreatBullet(Vector3 dir, Vector3 creatPoint)//�����ӵ�
    {
        BulletCharacter bulletCharacter = Instantiate(bulletTemplate, creatPoint, Quaternion.identity);
        bulletCharacter.gameObject.SetActive(true);
        bulletCharacter.dir = dir;
        tempBullets.Add(bulletCharacter);
        return bulletCharacter;
    }
    public void ClearBulletsList()//����ӵ�
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
        animator.SetBool("IsSkill", false);//ֹͣ����
        animator.SetBool("IsChangePosition", true);//ֹͣ����     
    }//ֹͣ����
    public void moveTime0()
    {
        boss2.moveCount = 0;
    }//�ƶ�ʱ������ֹbug����
}
