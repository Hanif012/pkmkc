using UnityEngine;
using TMPro;
using System.Globalization;
using System;



public class Bank : MonoBehaviour
{
    [SerializeField] private GameObject StrukBeli;
    [SerializeField] private GameObject StrukJual;
    [SerializeField] private GameObject prefabBankButton;
    [SerializeField] private GameObject ButtonContainer;
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
        CreateBankButton(Sender, SenderAccount, amount, "Deposit");
        balance += amount;
        UpdateBalance();
    }

    public bool Withdraw(int amount, string Receiver, string ReceiverAccount)
    {
        if (balance < amount)
        {
            Debug.Log("Insufficient funds.");
            balance -= amount;
            UpdateBalance();
            return false;
        }

        CreateBankButton(Receiver, ReceiverAccount, amount, "Withdraw");
        balance -= amount;
        UpdateBalance();
        return true;
    }

    private void FetchBalance()
    {
        string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        balanceText.text = $"Balance:\nRp {formattedBalance}";
    }

    private void UpdateBalance()
    {
        string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        balanceText.text = $"Balance:\nRp {formattedBalance}";
    }

    private void CreateBankButton(string provider, string providerAccount, int nominal, string transactionType)
    {
        GameObject bankButton = Instantiate(prefabBankButton, ButtonContainer.transform);

        BankPress bankPress = bankButton.GetComponent<BankPress>();
        if (bankPress != null)
        {
            bankPress.GenerateReceipt(provider, providerAccount, nominal, transactionType);
        }
        else
        {
            Debug.LogError("BankPress component not found on instantiated button.");
        }
    }

}
