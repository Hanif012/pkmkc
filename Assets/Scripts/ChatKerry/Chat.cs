using System.Collections.Generic;

public class Chat
{
    public string ChatId { get; set; }
    public string ChatName { get; set; } // For group chats
    public List<User> Participants { get; set; }
    public List<Message> Messages { get; set; } = new List<Message>();
    public bool IsGroupChat { get; set; }

    public Chat(string chatId, string chatName, bool isGroupChat, List<User> participants)
    {
        ChatId = chatId;
        ChatName = chatName;
        IsGroupChat = isGroupChat;
        Participants = participants;
    }

    // Methods to manage participants, messages, etc.
    public void AddMessage(Message message)
    {
        Messages.Add(message);
    }

    public void AddParticipant(User user)
    {
        Participants.Add(user);
    }
}
