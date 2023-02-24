using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talktive : MonoBehaviour
{
    public float waitTime;
    public GameObject[] items;

    [TextArea(1, 3)]
    public string[] Lines;
    [SerializeField]
    private int dialogAmount = 0;


    private void Start()
    {
        if (dialogAmount == 1)
        {

            Invoke("GetItems", waitTime);
        }
        else if (dialogAmount >= 2)
        {

            Invoke("GetItems", waitTime);
        }
    }
    private void OnTriggerEnter2D(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            merchantDialog.instance.isDialog= true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            merchantDialog.instance.isDialog= false;
            dialogAmount++;
        }
    }
    private void Update()
    {
        if(merchantDialog.instance.isDialog && Input.GetKeyDown(KeyCode.Escape) &&
            merchantDialog.instance.dialogBox.activeInHierarchy == false)
        {
            merchantDialog.instance.showDialog(Lines);
        }
    }
}
