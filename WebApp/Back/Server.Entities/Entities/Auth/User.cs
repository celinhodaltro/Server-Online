namespace Server.Entities
{
    public class User : DefaultDb
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

    }
}