using UnityEngine;

public class Contact
{
    public User ContactUser { get; set; }
    public bool IsFavorite { get; set; } // For adding to favorites
    public GameManager.GameTime AddedDate { get; set; }

    public Contact(User contactUser, bool isFavorite, GameManager.GameTime addedDate)
    {
        ContactUser = contactUser;
        IsFavorite = isFavorite;
        AddedDate = addedDate;
    }
}
