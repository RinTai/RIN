using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class merchantDialog : MonoBehaviour
{
    public float waitTime;
    public GameObject[] items;

    public Image dialogBox;
    public Text dialogBoxText;
    public string[] dialogs;

    private bool isDialog;
    private int dialogAmount = 0;

    void Start()
    {
        dialogBox.gameObject.SetActive(false);
    }

    
    void Update()
    {
        if(isDialog && Input.GetKeyDown(KeyCode.Space))
        {
            dialogBox.gameObject.SetActive(true);

            if(dialogAmount == 1)
            {
                dialogBoxText.text = dialogs[0];
                Invoke("GetItems", waitTime);
            }
            else if(dialogAmount >= 2)
            {
                dialogBoxText.text = dialogs[1];
                Invoke("GetItems", waitTime);
            }
        }
        else if(!isDialog)
        {
            dialogBox.gameObject.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            dialogAmount++;
            isDialog = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isDialog= false;
        }
    }
    void GetItems()
    {
        int rand = Random.Range(0, items.Length);
        Instantiate(items[rand], transform.position, Quaternion.identity);
    }
}
