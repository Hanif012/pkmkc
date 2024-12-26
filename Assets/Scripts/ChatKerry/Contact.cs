using System.Drawing; // Remove this if not needed
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Contact: MonoBehaviour
{
    [SerializeField] private string TextName = "Mom";
    [SerializeField] private Sprite ContactImage;
    [SerializeField] private GameObject NameObj;
    [SerializeField] private GameObject ImageObj;
    [SerializeField] private GameObject LatestChat;
    [SerializeField] private GameObject ChatObj;
    void Awake()
    {
        if(NameObj == null)
        {
            Debug.Log("ChatObj is null");
            NameObj = GameObject.Find("Name");         
        }
        if(ImageObj == null)
        {
            Debug.Log("ChatObj is null");
            ImageObj = GameObject.Find("Image");         
        }
        ContactSetup();
    }

    // Update the Display
    public void ContactSetup()
    {
        ImageObj.GetComponent<UnityEngine.UI.Image>().sprite = ContactImage;
        NameObj.GetComponent<TextMeshProUGUI>().text = TextName;
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
