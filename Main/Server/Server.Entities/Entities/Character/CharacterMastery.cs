using Server.Entities;
using Server.Entities.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;
public class CharacterMastery :DefaultDb
{
    public int? Id { get; set; }
    public CharacterMasteryType MasteryType { get; set; }
    public string Name { get; set; } = string.Empty;

    public int CharacterId { get; set; }
    [ForeignKey("CharacterId")]
    public virtual Character? Character { get; set; }
}