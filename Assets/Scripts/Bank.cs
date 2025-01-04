using UnityEngine;
using TMPro;
using System.Globalization;

public class Bank : MonoBehaviour
{
    public int balance = 100000;
    public TextMeshProUGUI balanceText;
    [SerializeField] private GameObject prefabBankButton;
    
    void Start()
    {
        FetchBalance();
    }

    public void Deposit(int amount, string message)
    {
        if (amount < 0)
        {
            Debug.Log("Invalid amount.");
            return;
        }
        balance += amount;
        UpdateBalance(message);
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
        string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        balanceText.text = $"Balance:\nRp {formattedBalance}";

        GameObject bankButton = Instantiate(prefabBankButton, transform);
        bankButton.GetComponent<StrukButton>().orderDetails.OrderNames.Add(message);
    }

    public void OnBankButtonClicked()
    {
        Debug.Log("Bank button clicked.");
        transform.SetAsLastSibling();
    }
}
