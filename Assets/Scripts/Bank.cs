using UnityEngine;
using TMPro;
using System.Globalization;
using System;

public class ReceiptReceive
{
    public string ReceiverName { get; set; }
    public string ReceiverLocation { get; set; }
    public DateTime TransactionDate { get; set; }
    public TimeSpan TransactionTime { get; set; }
    public decimal TransactionAmount { get; set; }
    public string ReferenceNumber { get; set; }
    public string QRISReferenceNumber { get; set; }
    public string MerchantPAN { get; set; }
    public string CustomerPAN { get; set; }
    public string AcquirerBank { get; set; }
    public string TerminalID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAccount { get; set; }
}

public class ReceiptSend
{
    public string ProviderName { get; set; }
    public string ProviderAccount { get; set; }
    public DateTime TransactionDate { get; set; }
    public TimeSpan TransactionTime { get; set; }
    public decimal TopUpAmount { get; set; }
    public decimal TransactionFee { get; set; }
    public decimal TotalTransactionAmount { get; set; }
    public string ReferenceNumber { get; set; }
    public string CustomerName { get; set; }
    public string CustomerAccount { get; set; }
}



public class Bank : MonoBehaviour
{
    [SerializeField] private GameObject StrukBeli;
    [SerializeField] private GameObject StrukJual;
    [SerializeField] private GameObject prefabBankButton;

    public int balance = 100000;
    public TextMeshProUGUI balanceText;

    public GameManager gameManager;
    public void Awake()
    {
        if(StrukBeli == null)
        {
            Debug.Log("StrukBeli is null");
        }

        if(StrukJual == null)
        {
            Debug.Log("StrukJual is null");
        }
    }
    void Start()
    {
        FetchBalance();
    }

    public void Deposit(int amount, string Sender, string SenderAccount)
    {
        if (amount < 0)
        {
            Debug.Log("Invalid amount.");
            return;
        }
        CreateBankButton(Sender, amount, "Deposit", );
        balance += amount;
        UpdateBalance(Sender);
    }

    public bool Withdraw(int amount, string message, string Receiver, string ReceiverAccount)
    {
        if (balance < amount)
        {
            Debug.Log("Insufficient funds.");
            balance -= amount;
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
        string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        balanceText.text = $"Balance:\nRp {formattedBalance}";
    }

    private void CreateBankButton(string provider, string providerAccount, int nominal, string transactionType)
    {
        GameObject bankButton = Instantiate(prefabBankButton, transform);
        bankButton.GetComponent<BankPress>().GenerateReceipt(provider, providerAccount, nominal, transactionType);
    }
}
