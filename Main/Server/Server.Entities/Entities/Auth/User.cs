using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mail;
using System.Text.Json.Serialization;

namespace Server.Entities;

public class User : DefaultDb
{
    public string? Email { get; set; }
    public string? Password { get; set; }

    public UserInfo? UserInfo { get; set; }

    
    public ICollection<Character>? Characters { get; set; }
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