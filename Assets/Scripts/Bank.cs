using UnityEngine;
using TMPro;
using System.Globalization;
using System;
    public class ReceiptReceive
    {
        public string ReceiverName { get; set; } // e.g., IDM QRIS LIVIN
        public string ReceiverLocation { get; set; } // e.g., Jakarta Utara - ID

        // Transaction Details
        public DateTime TransactionDate { get; set; } // e.g., 2 Dec 2024
        public TimeSpan TransactionTime { get; set; } // e.g., 21:20:25 WIB
        public decimal TransactionAmount { get; set; } // e.g., Rp 12,600.00
        public string ReferenceNumber { get; set; } // e.g., 412020635687
        public string QRISReferenceNumber { get; set; } // e.g., 433721959758
        public string MerchantPAN { get; set; } // e.g., 9360000071017497204
        public string CustomerPAN { get; set; } // e.g., 93600000812105306036

        // Acquirer Details
        public string AcquirerBank { get; set; } // e.g., Bank Mandiri
        public string TerminalID { get; set; } // e.g., 73369767

        // Source Details
        public string CustomerName { get; set; } // e.g., FATHI NAUFAL HANIF
        public string CustomerAccount { get; set; } // e.g., ****0603
    }


    public class ReceiptSend
    {
        public string ProviderName { get; set; } // e.g., PLN Prabayar
        public string ProviderAccount { get; set; } // e.g., ****1012

        // Transaction Details
        public DateTime TransactionDate { get; set; } // e.g., 1 Jan 2025
        public TimeSpan TransactionTime { get; set; } // e.g., 13:55:18 WIB
        public decimal TopUpAmount { get; set; } // e.g., Rp 100,000.00
        public decimal TransactionFee { get; set; } // e.g., Rp 3,500.00
        public decimal TotalTransactionAmount { get; set; } // e.g., Rp 103,500.00
        public string ReferenceNumber { get; set; } // e.g., 702501011355131131

        // Customer Details
        public string CustomerName { get; set; } // e.g., FATHI NAUFAL HANIF
        public string CustomerAccount { get; set; } // e.g., ****0603
    }
public class Bank : MonoBehaviour
{


    [SerializeField] private GameObject StrukBeli;
    [SerializeField] private GameObject StrukJual;
    [SerializeField] private GameObject prefabBankButton;

    public int balance = 100000;
    public TextMeshProUGUI balanceText;

    enum ReceiptType
    {
        BELI,
        JUAL,
        NULL
    }

    private ReceiptType receiptType = ReceiptType.NULL;

    void Start()
    {
        FetchBalance();
    }

    public void Deposit(int amount, string Sender)
    {
        if (amount < 0)
        {
            Debug.Log("Invalid amount.");
            return;
        }
        balance += amount;
        UpdateBalance(Sender);
    }

    public bool Withdraw(int amount, string message)
    {
        if (balance < amount)
        {
            Debug.Log("Insufficient fundss.");
            balance -= amount;
            // TODO: Possible State Change
            UpdateBalance(message);
            return false;
        }

        balance -= amount;
        UpdateBalance(message);
        return true;
    }

    private void FetchBalance()
    {
        string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        balanceText.text = $"Balance:\nRp {formattedBalance}";
    }

    private void UpdateBalance(string message)
    {
        // string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        // balanceText.text = $"Balance:\nRp {formattedBalance}";

        // GameObject bankButton = Instantiate(prefabBankButton, transform);
        // bankButton.GetComponent<StrukButton>()
    }

    public void OnBankButtonClicked()
    {
        Debug.Log("Bank button clicked.");
        transform.SetAsLastSibling();
    }

    private void ReceiptSelection()
    {
        switch (receiptType)
        {
            case ReceiptType.BELI:
                StrukBeli.SetActive(true);
                StrukJual.SetActive(false);
                break;
            case ReceiptType.JUAL:
                StrukJual.SetActive(true);
                StrukBeli.SetActive(false);
                break;
            case ReceiptType.NULL:
                StrukBeli.SetActive(false);
                StrukJual.SetActive(false);
                // Debug.Log("Receipt not Selected");
                break;
        }
    }
}
