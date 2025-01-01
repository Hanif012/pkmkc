using System.Drawing; // Remove this if not needed
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Contact: MonoBehaviour
{
    [SerializeField] private GameObject NameObj;
    [SerializeField] private GameObject ImageObj;
    [SerializeField] private GameObject LatestChat;
    [SerializeField] private GameObject ChatObj;
    void Awake()
    {
        if(NameObj == null)
        {
            NameObj = GameObject.Find("Name");         
        }
        if(ImageObj == null)
        {
            ImageObj = GameObject.Find("Image");         
        }
        // ContactSetup();
    }

    public void ContactSetup(Sprite sp, string txt)
    {
        ImageObj.GetComponent<UnityEngine.UI.Image>().sprite = sp;
        NameObj.GetComponent<TextMeshProUGUI>().text = txt;
    }
    public void SetLatestChat(string latestChat)
    {
        LatestChat.GetComponent<TextMeshProUGUI>().text = latestChat;
    }
    public void OnClick()
    {
        ChatObj.SetActive(true);
        ChatObj.transform.SetAsLastSibling();
        Debug.Log("Contact clicked.");
    }
}