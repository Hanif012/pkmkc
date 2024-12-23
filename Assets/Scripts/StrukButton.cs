using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

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
    public TextMeshProUGUI textMeshProUGUI;
    public struk orderDetails = new struk();
    public TextMeshProUGUI ButtonText;

    void Awake()
    {
        ButtonText = GetComponent<TextMeshProUGUI>();
        ButtonText.text = $"{orderDetails.Date}";    
    }
    void Update()
    {
        if (isStrukButton)
        {
            GenerateReceipt();
            isStrukButton = false;
        }
        else
        {
            textMeshProUGUI.text = "Struk";
        }
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

        textMeshProUGUI.text = receiptText;
    }
    
}
