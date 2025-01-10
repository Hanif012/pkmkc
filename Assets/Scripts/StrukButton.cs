using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Globalization;

public class BankPress : MonoBehaviour
{
    public class Transaction
    {
        public string date;
        public string time;
        public string nominal;
        public string biaya;
        public string total;
        public string noRef;
    }

    public enum ReceiptType
    {
        BELI,
        JUAL,
        NULL
    }

    public ReceiptType receiptType = ReceiptType.NULL;
    public class StrukData
    {
        public string Header;
        public string CustomerName;
        public string Statement;
        public string PenyediaJasa;
        public string ProviderName;
        public string ProviderAccount;
        public string Transaction;
        public string Statement1;
        public string CustomerName1;
        public string CustomerAccount;
    }

    
    public bool isStrukButton = false;
    public Text ButtonText = null;
    public Bank bank;
    GameManager gameManager;

    [SerializeField] public string ButtonTextString = "DateHere";
    [SerializeField] public GameObject StrukBeli;
    [SerializeField] public GameObject StrukJual;
    Transaction transaksi;

    void Awake()
    {
        

        if (bank == null)
        {
            bank = FindObjectOfType<Bank>();
        }

        if(StrukBeli == null)
        {
            Debug.Log("StrukBeli is null");
        }

        if(StrukJual == null)
        {
            Debug.Log("StrukJual is null");
        }
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        CreateReceipt(receiptType);

        if (ButtonText == null)
        {
            ButtonText.text =  transaksi.time + transaksi.date;
        }
    }
    

    void Start()
    {
        if (ButtonText != null)
        {
            ButtonText.text = "Struk";
        }
        if(ReceiptType.BELI == receiptType)
        {
            StrukBeli.SetActive(true);
            StrukJual.SetActive(false);
        }
        else if(ReceiptType.JUAL == receiptType)
        {
            StrukBeli.SetActive(false);
            StrukJual.SetActive(true);
        }
        else
        {
            StrukBeli.SetActive(false);
            StrukJual.SetActive(false);
        }
    }
    private void CreateReceipt(ReceiptType receiptType)
    {
        if (receiptType == ReceiptType.BELI)
        {            
            StrukBeli.SetActive(true);
            StrukJual.SetActive(false);

        }
        else if (receiptType == ReceiptType.JUAL)
        {
            StrukBeli.SetActive(false);
            StrukJual.SetActive(true);
        }
        else
        {
            StrukBeli.SetActive(false);
            StrukJual.SetActive(false);
        }
    }

    //Deposit
    public void StrukJualActive(StrukData data)
    {
        // StrukJual.transform.Find("Header").GetComponent<TextMeshProUGUI>().text = data.Header;
        StrukJual.transform.Find("CustomerName").GetComponent<TextMeshProUGUI>().text = data.CustomerName;
        StrukJual.transform.Find("Statement").GetComponent<TextMeshProUGUI>().text = data.Statement;
        StrukJual.transform.Find("Penyedia Jasa").GetComponent<TextMeshProUGUI>().text = data.PenyediaJasa;
        StrukJual.transform.Find("ProviderName").GetComponent<TextMeshProUGUI>().text = data.ProviderName;
        StrukJual.transform.Find("ProviderAccount").GetComponent<TextMeshProUGUI>().text = data.ProviderAccount;
        StrukJual.transform.Find("Transaction").GetComponent<TextMeshProUGUI>().text = data.Transaction;
        StrukJual.transform.Find("Statement1").GetComponent<TextMeshProUGUI>().text = data.Statement1;
        StrukJual.transform.Find("CustomerName1").GetComponent<TextMeshProUGUI>().text = data.CustomerName1;
        StrukJual.transform.Find("CustomerAccount").GetComponent<TextMeshProUGUI>().text = data.CustomerAccount;
        Debug.Log("StrukJualActive");
    }
    //Withdraw
    public void StrukBeliActive(StrukData data)
    {
        // StrukBeli.transform.Find("Header").GetComponent<TextMeshProUGUI>().text = data.Header;
        StrukBeli.transform.Find("CustomerName").GetComponent<TextMeshProUGUI>().text = data.CustomerName;
        StrukBeli.transform.Find("Statement").GetComponent<TextMeshProUGUI>().text = data.Statement;
        StrukBeli.transform.Find("Penyedia Jasa").GetComponent<TextMeshProUGUI>().text = data.PenyediaJasa;
        StrukBeli.transform.Find("ProviderName").GetComponent<TextMeshProUGUI>().text = data.ProviderName;
        StrukBeli.transform.Find("ProviderAccount").GetComponent<TextMeshProUGUI>().text = data.ProviderAccount;
        StrukBeli.transform.Find("Transaction").GetComponent<TextMeshProUGUI>().text = data.Transaction;
        StrukBeli.transform.Find("Statement1").GetComponent<TextMeshProUGUI>().text = data.Statement1;
        StrukBeli.transform.Find("CustomerName1").GetComponent<TextMeshProUGUI>().text = data.CustomerName1;
        StrukBeli.transform.Find("CustomerAccount").GetComponent<TextMeshProUGUI>().text = data.CustomerAccount;
        Debug.Log("StrukBeliActive");
    }

    public void onClick()
    {
        Debug.Log("Struk button pressed.");
        if (receiptType == ReceiptType.BELI)
        {
            StrukBeli.SetActive(true);
            StrukJual.SetActive(false);
        }
        else if (receiptType == ReceiptType.JUAL)
        {
            StrukBeli.SetActive(false);
            StrukJual.SetActive(true);

        }
        else
        {
            StrukBeli.SetActive(false);
            StrukJual.SetActive(false);
        }
    }
    
    public void GenerateReceipt(string provider, string providerAccount, int nominal, string additionalInfo)
    {
        TransactionData(nominal);
        StrukData strukData = new StrukData
        {
            Header = "Top-up Berhasil",
            CustomerName = "Halo " + GameManager.Instance.GetPlayerName().ToUpper(),
            Statement = "Berikut adalah detail transaksi Anda:",
            PenyediaJasa = "Penerima:",
            ProviderName = provider.ToUpper(),
            ProviderAccount = "Provider Account " + CensorRekening(providerAccount),
           
            Transaction = "Tanggal\t\t: "+ transaksi.date + "\n" +
                          "Waktu\t\t: " + transaksi.time + "\n" +
                        //   "Nominal\t\t: " + nominal + "\n" +
                        //   "Biaya\t\t: " + "Rp 0" + "\n" +
                          "Total\t\t\t: " + "Rp " + nominal + "\n" +
                          "No. Ref\t\t: " + transaksi.noRef,

            Statement1 = "Rekening Sumber",
            CustomerName1 = GameManager.Instance.GetPlayerName().ToUpper(),
            CustomerAccount = "Costumer Account " + CensorRekening(GameManager.Instance.GetRekening()),
        };
        
        if (additionalInfo == "Deposit")
        {
            receiptType = ReceiptType.JUAL;
            StrukJualActive(strukData);
            Debug.Log("Deposit");
        }
        else if (additionalInfo == "Withdraw")
        {
            receiptType = ReceiptType.BELI;
            StrukBeliActive(strukData);
            Debug.Log("Withdraw");
        }
        else
        {
            Debug.LogError("Invalid additional info.");
        }
    }

    public void TransactionData(int total)
    {
        string formattedBalance = total.ToString("#,0", new CultureInfo("id-ID"));
        transaksi = new Transaction
        {
            date = GameManager.Instance.GetCurrentDate(),
            time = GameManager.Instance.GetCurrentTime(),
            // nominal = nominal,
            // biaya = biaya,
            total = formattedBalance,
            noRef = GenerateReferenceNumber()
        };
    }

    public string CensorRekening(string rekening)
    {
        string censored = "";
        for (int i = 0; i < rekening.Length; i++)
        {
            if (i < 3 || i >= rekening.Length - 3)
            {
                censored += rekening[i];
            }
            else
            {
                censored += "*";
            }
        }
        return censored;
    }

    public string GenerateReferenceNumber()
    {
        return UnityEngine.Random.Range(1000000000, 9999999999).ToString();
    }
}