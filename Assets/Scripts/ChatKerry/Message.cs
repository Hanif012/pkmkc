using UnityEngine;

public class Message : MonoBehaviour
{
    public string MessageId { get; set; }
    public string SenderId { get; set; }
    public string ChatId { get; set; }
    public string Text { get; set; }
    public GameTime SentTime { get; set; }
    public Attachment Attachment { get; set; } // Optional attachment
    // public List<Reaction> Reactions { get; set; } = new List<Reaction>();

    public Message(string messageId, string senderId, string chatId, string text, GameTime sentTime)
    {
        MessageId = messageId;
        SenderId = senderId;
        ChatId = chatId;
        Text = text;
        SentTime = sentTime;
    }

    // Methods to add reactions, etc.
    // public void AddReaction(Reaction reaction)
    // {
    //     Reactions.Add(reaction);
    // }
}
