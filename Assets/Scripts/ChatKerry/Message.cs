using UnityEngine;
using Yarn.Unity;
using Lean.Gui;
using System;

public class Message : MonoBehaviour
{
    [Header("Daily Mission")]
    [SerializeField] private DailyMission dailyMission;
    [Header("Contact")]
    [SerializeField] private GameObject ContactPrefab;
    [SerializeField] private GameObject ContactParent;

    [Header("Chat")]
    [SerializeField] private GameObject ChatPrefab;
    [SerializeField] private GameObject ChatParent;

    void Awake()
    {

        // FetchDaily();
    }

    private void FetchDaily()
    {
        if (dailyMission == null)
        {
            Debug.LogError("Daily Mission is missing.");
            
        }

        foreach (var quest in dailyMission.quests)
        {
            FetchData(quest.NPCName, quest.NPCImage, quest.DialogueNode);
        }
    }

    public void Start()
    {
        // GetComponent<LeanWindow>().TurnOff();
    }
    public void FetchData(string contactName, Sprite contactSprite, string nodeStarter = "Start")
    {
        var chat = AddChat(nodeStarter);
        var contact = AddContact(contactName, contactSprite);
        contact.GetComponent<LeanButton>().OnClick.AddListener(() => chat.transform.SetAsLastSibling());
    }

    public GameObject AddContact(string contactName = "Contact Name", Sprite contactSprite = null)
    {
        GameObject contact = Instantiate(ContactPrefab);
        contact.transform.SetParent(ContactParent.transform, false);
        contact.SetActive(true);
        contact.GetComponent<Contact>().ContactSetup(contactSprite, contactName);
        Debug.Log($"Added {contactName} to Contact.");
        return contact;
    }
    public GameObject AddChat(string nodeStarter = "Start")
    {
        if(ChatParent == null)
        {
            ChatParent = this.gameObject;
        }
        GameObject chat = Instantiate(ChatPrefab);
        chat.transform.SetParent(ChatParent.transform, false);
        chat.transform.Find("Dialogue Runner").GetComponent<DialogueRunner>().startAutomatically = true; 
        chat.transform.Find("Dialogue Runner").GetComponent<DialogueRunner>().startNode = nodeStarter;
        chat.SetActive(true);
        return chat;
    }

    public void FetchGameManagerToMessages(DailyMission mission)
    {
        dailyMission = mission;
        FetchDaily();
    }

    public void OnMessageButtonClicked()
    {
        Debug.Log("Message button clicked.");
        transform.SetAsLastSibling();
    }

}
