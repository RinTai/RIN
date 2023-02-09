using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TressureBox : MonoBehaviour
{
    public GameObject[] items;
    public float waitTime;

    private Animator anim;
    private bool isOpen;
    private bool opened;
    
    void Start()
    {
        anim= GetComponent<Animator>();
        isOpen= false;
    }

    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(isOpen && !opened)
            {
                anim.SetTrigger("open");
                opened= true;
                Invoke("GetItems", waitTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isOpen = false;
        }
    }
    void GetItems()
    {
        int rand = Random.Range(0, items.Length);
        Instantiate(items[rand],transform.position,Quaternion.identity);
    }
}
