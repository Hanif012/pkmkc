using Unity.VisualScripting;
using UnityEngine;

public class Message : MonoBehaviour
{
    // PREFABS
    [Header("Contact")]
    public GameObject ContactPrefab;
    public GameObject ContactParent;

    [Header("Chat")]
    public GameObject ChatPrefab;
    public GameObject ChatParent;
    void Awake()
    {
        AddContact();
        AddChat();
    }
    public void AddContact()
    {
        GameObject contact = Instantiate(ContactPrefab);
        contact.transform.SetParent(ContactParent.transform, false);
    }

    public void AddChat()
    {
        GameObject chat = Instantiate(ChatPrefab);
        
        if (ChatParent != null)
        {
            chat.transform.SetParent(ChatParent.transform, false);
        }
        else
        {
            chat.transform.SetParent(this.transform, false);
        }
    }

    // public void AddEnemyAsChild(GameObject enemyPrefab, GameObject parent)
    // {
    //     GameObject enemy = Instantiate(enemyPrefab);
    //     enemy.transform.SetParent(parent.transform, false);
    // }
}
