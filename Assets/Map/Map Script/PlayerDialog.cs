using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDialog : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] dialog1;
    [TextArea(1, 3)]
    public string[] dialog2;
    [TextArea(1, 3)]
    public string[] dialog3;
    [TextArea(1, 3)]
    public string[] dialog4;

    public int dialogCount = 0;

    private bool startDialog;

    private void Update()
    {
        if(startDialog)
        {
            StartCoroutine(StartDialog(dialog1));
            startDialog = false;
        }else if(FindObjectOfType<bossGeneration>().dialogGeneration == true)
        {
            StartCoroutine(StartDialog(dialog2));
            FindObjectOfType<bossGeneration>().dialogGeneration = false;
        }
        else if(dialogCount ==3)
        {
            StartCoroutine(StartDialog(dialog3));
        }else if(dialogCount == 4)
        {
            StartCoroutine(StartDialog(dialog4));
        }
    }

    IEnumerator StartDialog(string[] newLines)
    {
        merchantDialog.instance.showDialog(newLines);
        yield return new WaitForSeconds(3f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dialogCenter"))
        {
            startDialog= true;
            FindObjectOfType<mapGeneration>().dialogCenter.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("dialogCenter"))
        {
            startDialog= false;
        }
    }
}
