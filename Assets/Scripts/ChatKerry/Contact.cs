using UnityEngine;

public class Contact
{
    public User ContactUser { get; set; }
    public bool IsFavorite { get; set; } // For adding to favorites
    public GameTime AddedDate { get; set; }

    public Contact(User contactUser, bool isFavorite, GameTime addedDate)
    {
        ContactUser = contactUser;
        IsFavorite = isFavorite;
        AddedDate = addedDate;
    }
}
