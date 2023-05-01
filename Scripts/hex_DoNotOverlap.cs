using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hex_DoNotOvelap : MonoBehaviour
{
    public hex_Object hexobj;
    // 
    void Awake()
    {
        GameObject hexManager = GameObject.Find("Hex Manager");
        hexobj = hexManager.GetComponent<hex_Object>();
        }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
        {
        if (hexobj.hex_isTouchingHex(this.gameObject) != "") Destroy(this.gameObject);
        }
    }
