using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class StrukButton : MonoBehaviour
{
    [System.Serializable]
    public class struk
    {
        public string Date = "2024-04-14";
        public int OrderNo = 1;
        public List<string> OrderNames = new List<string> { "Lorem Ipsum" };
        public List<int> Quantities = new List<int> { 1 };
        public List<int> Prices = new List<int> { 12000 };
    }
    public bool isStrukButton;
    public TextMeshProUGUI ReceiptText;
    public struk orderDetails = new struk();
    public Text ButtonText;
    public string strukText = "Struk";
    public Bank bank;
    public OrderClass orderClass;

    void Awake()
    {
        ButtonText = transform.GetComponentInChildren<Text>();

        this.ButtonText.text = $"{orderDetails.Date}";
        if(ReceiptText == null)
        {
            Debug.Log("Struk text not found, using default Struk text.");
            // DO NOT CHANGE THE HEIRARCY! IM TOO LAZY TO USE TAGS
            ReceiptText = transform.parent.parent.Find(strukText).GetComponent<TextMeshProUGUI>();
        }
        bank = FindObjectOfType<Bank>();
        orderClass = FindObjectOfType<OrderClass>();
    }
    public void onClick()
    {
        GenerateReceipt();
        Debug.Log("Struk button pressed.");
    }

    void GenerateReceipt()
    {
        string receiptText = $"{orderDetails.Date}\t\tOrder No. {orderDetails.OrderNo}\n" +
                             "------------------------------------------------\n";

        int total = 0;
        for (int i = 0; i < orderDetails.OrderNames.Count; i++)
        {
            string orderName = orderDetails.OrderNames[i];
            int quantity = orderDetails.Quantities[i];
            int price = orderDetails.Prices[i];
            int subtotal = quantity * price;
            total += subtotal;

            receiptText += $"{orderName}\n" +
                           $"{quantity} X {price.ToString("N0", new CultureInfo("id-ID"))}\t\tRp {subtotal.ToString("N0", new CultureInfo("id-ID"))}\n";
        }

        receiptText += "------------------------------------------------\n" +
                       $"Total\t\t\tRp {total.ToString("N0", new CultureInfo("id-ID"))}";

        ReceiptText.text = receiptText;
    }

}
