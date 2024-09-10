using System.Net.Mail;

namespace Server.Entities;

public class User : DefaultDb
{
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UserInfo UserInfo { get; set; }

    public UserType UserType { get; set; }

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(Email) &&
               !string.IsNullOrWhiteSpace(Password);
    }

}

public enum UserType
{
    Player,
    Tutor,
    SeniorTutor,
    GameMaster,
    God
}