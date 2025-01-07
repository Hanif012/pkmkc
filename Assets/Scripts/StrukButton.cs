using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;
using System;

public class StrukButton : MonoBehaviour
{

    public bool isStrukButton = false;
    public Text ButtonText = null;
    public ReceiptReceive receiptReceive;
    public ReceiptSend receiptSend;
    public Bank bank;


    void Awake()
    {
        ButtonText = transform.GetComponentInChildren<Text>();
        bank = FindObjectOfType<Bank>();
    }

    public void onClick()
    {
        GenerateReceipt();

        Debug.Log("Struk button pressed.");
    }

    void GenerateReceipt()
    {
        if (isStrukButton)
        {
            
        }
    }

}
