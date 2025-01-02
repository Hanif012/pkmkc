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
        UpdateBalance();
    }

    public void Deposit(int amount)
    {
        balance += amount;
        UpdateBalance();
    }

    public bool Withdraw(int amount)
    {
        if (balance < amount)
        {
            Debug.Log("Insufficient fundss.");
            balance -= amount;
            // TODO: Possible State Change
            UpdateBalance();
            return false;
        }
        
        balance -= amount;
        UpdateBalance();
        return true;
    }

    private void UpdateBalance()
    {
        string formattedBalance = balance.ToString("#,0", new CultureInfo("id-ID"));
        balanceText.text = $"Balance:\nRp {formattedBalance}";
    }

    public void OnBankButtonClicked()
    {
        Debug.Log("Bank button clicked.");
        transform.SetAsLastSibling();
    }
}
