using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class merchantDialog : MonoBehaviour
{
    public static merchantDialog instance; 

    

    public GameObject dialogBox;
    public Text dialogBoxText;

    [TextArea(1,3)]
    public string[] dialogs;
    public bool isDialog;

    


    private int currentLines;

    private void Awake()
    {
        instance= this;
    }

    void Start()
    {
        dialogBox.SetActive(false);
    }

    
    void Update()
    {
        
        
        if (dialogBox.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                currentLines++;

                if (currentLines < dialogs.Length)
                    dialogBoxText.text = dialogs[currentLines];
                else
                    dialogBox.SetActive(false);
            }
        }
    }
    
    public void showDialog(string[] newLines)
    {
        dialogs = newLines;
        currentLines = 0;
        dialogBoxText.text = dialogs[currentLines];
        dialogBox.SetActive(true);
    }
    
}
