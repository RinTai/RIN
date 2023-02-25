using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossGeneration : MonoBehaviour
{
    public GameObject bossPos;//记录boss位置
    public GameObject Boss;//boss生成
    public bool dialogGeneration;

    private bool beginGeneration;
    private int beginAmount = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            beginAmount++;
        }
        
        if(beginGeneration && beginAmount == 1)
        {
            dialogGeneration = true;
            GameObject.Instantiate(Boss);
            beginGeneration= false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            beginGeneration= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            beginGeneration= false;
        }
    }

}
