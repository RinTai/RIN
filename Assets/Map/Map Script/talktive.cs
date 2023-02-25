using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class talktive : MonoBehaviour
{
    public float waitTime;
    public GameObject[] items;

    [TextArea(1, 3)]
    public string[] Lines;
    [TextArea(1,3)]
    public string[] Lines2;
    [TextArea(1,3)]
    public string[] Lines3;
    [SerializeField]
    private int dialogAmount = 0;
    [SerializeField]
    private bool isEntered;


    private void Start()
    {
        if (dialogAmount == 1)
        {

            Invoke("GetItems", waitTime);
        }
        else if (dialogAmount == 2)
        {

            Invoke("GetItems", waitTime);
        }
        else if(dialogAmount >= 3)
        {

            Invoke("GetItems", waitTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isEntered= true;
            dialogAmount++;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isEntered= false;
        }
    }
    private void Update()
    {
        if(isEntered && Input.GetKeyDown(KeyCode.Space) &&
            merchantDialog.instance.dialogBox.activeInHierarchy == false)
        {
            if(dialogAmount == 1)
            {
                merchantDialog.instance.showDialog(Lines);
            }
            else if(dialogAmount == 2)
            {
                merchantDialog.instance.showDialog(Lines2);
            }
            else if(dialogAmount >= 3)
            {
                merchantDialog.instance.showDialog(Lines3);
            }
        }
    }
}
