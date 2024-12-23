using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class Bank : MonoBehaviour
{
    public int balance = 100000;
    public TextMeshProUGUI balanceText;

    void Start()
    {
        UpdateBalance();
    }

    public void Deposit(int amount)
    {
        balance += amount;
        UpdateBalance();
    }

    public void Withdraw(int amount)
    {
        if (balance >= amount)
        {
            balance -= amount;
            UpdateBalance();
        }
        else
        {
            Debug.Log("Insufficient funds.");
        }
    }

    private void UpdateBalance()
    {
        string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        balanceText.text = $"Balance:\nRp {formattedBalance}";
    }
}
