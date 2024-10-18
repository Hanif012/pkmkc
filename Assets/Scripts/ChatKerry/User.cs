using UnityEngine;
public class User: MonoBehaviour
{
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string ProfilePictureUrl { get; set; }
    public string StatusMessage { get; set; } // e.g., "Online", "Away", "Do not disturb"
    public GameTime LastSeen { get; set; }

    // Additional properties
    public bool IsOnline { get; set; }
    public string PhoneNumber { get; set; }

    public User(string userId, string userName, string profilePictureUrl, string statusMessage, bool isOnline)
    {
        UserId = userId;
        UserName = userName;
        ProfilePictureUrl = profilePictureUrl;
        StatusMessage = statusMessage;
        IsOnline = isOnline;
    }
}
