using KnightsAndDragons.Core.Contracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KnightsAndDragons.Core.Models
{
    public class Knight : IKnight
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        public int AttackPower { get; set; }

        [Required]
        public int Health { get; set; }

        [Required]
        public int Mana { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public int Experience { get; set; }

        [Required]
        public int Gold { get; set; }
    }
}
