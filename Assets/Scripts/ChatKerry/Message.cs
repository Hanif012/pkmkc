using UnityEngine;
using Yarn.Unity;
using Lean.Gui;

public class Message : MonoBehaviour
{
    [Header("Chat Settings")]
    // [SerializeField] 
    [Header("Message Dialouge")]
    [SerializeField] private string NodeStarter = "Start";

    [Header("Contact")]
    [SerializeField] private GameObject ContactPrefab;
    [SerializeField] private GameObject ContactParent;

    [Header("Chat")]
    [SerializeField] private GameObject ChatPrefab;
    [SerializeField] private GameObject ChatParent;

    void Awake()
    {
        FetchData();
    }

    public void FetchData()
    {
        var chat = AddChat();
        var contact = AddContact();
        contact.GetComponent<LeanButton>().OnClick.AddListener(() => chat.transform.SetAsLastSibling());
    }

    public GameObject AddContact()
    {
        GameObject contact = Instantiate(ContactPrefab);
        contact.transform.SetParent(ContactParent.transform, false);
        contact.SetActive(true);
        return contact;
    }
    public GameObject AddChat()
    {
        if(ChatParent == null)
        {
            ChatParent = this.gameObject;
        }
        GameObject chat = Instantiate(ChatPrefab);
        chat.transform.SetParent(ChatParent.transform, false);
        chat.transform.Find("Dialogue Runner").GetComponent<DialogueRunner>().startAutomatically = true; // Start the chat automatically if not started :skull:
        chat.transform.Find("Dialogue Runner").GetComponent<DialogueRunner>().startNode = NodeStarter;
        chat.SetActive(true);
        return chat;
    }
}
